﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// A rule set is a collection of validation rules to be used to 
/// when validating the instance.
/// </summary>
public sealed class ValidationRuleStack : IValidationRuleStack
{
	private int size;
	private int version;
	private IValidationRule[] array;
	

	bool ICollection.IsSynchronized => false;
	object ICollection.SyncRoot => this;


	public ValidationRuleStack()
	{
		array = Array.Empty<IValidationRule>();
	}

	public ValidationRuleStack(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException("capacity", capacity, "Capacity must be greater than 0.");
		}
		this.array = new IValidationRule[capacity];
	}

	public ValidationRuleStack(IEnumerable<IValidationRule> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException("collection");
		}
		this.array = ToArray(collection, out size);
	}


	public int Count => size;

	public void Clear()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<IValidationRule>())
		{
			Array.Clear(array, 0, size);
		}
		size = 0;
		version++;
	}

	public bool Contains(IValidationRule item)
	{
		if (size != 0)
		{
			return Array.LastIndexOf(array, item, size - 1) != -1;
		}
		return false;
	}

	public void CopyTo(IValidationRule[] array, int arrayIndex)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (arrayIndex < 0 || arrayIndex > array.Length)
		{
			throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "The index is either less than 0 or greater than the array.");
		}
		if (array.Length - arrayIndex < size)
		{
			throw new ArgumentException("The size of the array is less than the current size.");
		}
		int num = 0;
		int num2 = arrayIndex + size;
		while (num < size)
		{
			array[--num2] = this.array[num++];
		}
	}

	void ICollection.CopyTo(Array array, int arrayIndex)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (array.Rank != 1)
		{
			throw new ArgumentException("Multi-dimension arrays not supported.", "array");
		}
		if (array.GetLowerBound(0) != 0)
		{
			throw new ArgumentException("Non-zero lower-bound arrays no supported.", "array");
		}
		if (arrayIndex < 0 || arrayIndex > array.Length)
		{
			throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "The index is either less than 0 or greater than the array.");
		}
		if (array.Length - arrayIndex < size)
		{
			throw new ArgumentException("The size of the array is less than the current size.");
		}
		try
		{
            Array.Copy(this.array, 0, array, arrayIndex, size);
			Array.Reverse(array, arrayIndex, size);
		}
		catch (ArrayTypeMismatchException)
		{
			throw new ArgumentException("The array type is invalid", "array");
		}
	}


	public void TrimExcess()
	{
		int num = (int)((double)array.Length * 0.9);
		if (size < num)
		{
			Array.Resize(ref array, size);
			version++;
		}
	}

	IValidationRule IValidationRuleStack.Peek()
	{
		int num = size - 1;
		IValidationRule[] array = this.array;
		if ((uint)num >= (uint)array.Length)
		{
			ThrowForEmptyStack();
		}
		return array[num];
	}

	bool IValidationRuleStack.TryPeek([MaybeNullWhen(false)] out IValidationRule result)
	{
		int num = size - 1;
		IValidationRule[] array = this.array;
		if ((uint)num >= (uint)array.Length)
		{
			result = default(IValidationRule);
			return false;
		}
		result = array[num];
		return true;
	}


	IValidationRule IValidationRuleStack.Pop()
	{
		int num = size - 1;
		IValidationRule[] array = this.array;
		if ((uint)num >= (uint)array.Length)
		{
			ThrowForEmptyStack();
		}
		version++;
		size = num;
		IValidationRule result = array[num];
		if (RuntimeHelpers.IsReferenceOrContainsReferences<IValidationRule>())
		{
			array[num] = default;
		}
		return result;
	}

	bool IValidationRuleStack.TryPop([MaybeNullWhen(false)] out IValidationRule result)
	{
		int num = size - 1;
		IValidationRule[] array = this.array;
		if ((uint)num >= (uint)array.Length)
		{
			result = default;
			return false;
		}
		version++;
		size = num;
		result = array[num];
		if (RuntimeHelpers.IsReferenceOrContainsReferences<IValidationRule>())
		{
			array[num] = default;
		}
		return true;
	}

	void IValidationRuleStack.Push(IValidationRule item)
	{
		int size = this.size;
		IValidationRule[] array = this.array;
		if ((uint)size < (uint)array.Length)
		{
			array[size] = item;
			version++;
			this.size = size + 1;
		}
		else
		{
			PushWithResize(item);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void PushWithResize(IValidationRule item)
	{
		Grow(size + 1);
		array[size] = item;
		version++;
		size++;
	}

	public int EnsureCapacity(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException("capacity", capacity, "Capacity must be greater than 0.");
		}
		if (array.Length < capacity)
		{
			Grow(capacity);
			version++;
		}
		return array.Length;
	}

	private void Grow(int capacity)
	{
		int num = ((array.Length == 0) ? 4 : (2 * array.Length));
		if ((uint)num > Array.MaxLength)
		{
			num = Array.MaxLength;
		}
		if (num < capacity)
		{
			num = capacity;
		}
		Array.Resize(ref array, num);
	}

	public IValidationRule[] ToArray()
	{
		if (size == 0)
		{
			return Array.Empty<IValidationRule>();
		}
		IValidationRule[] array = new IValidationRule[size];
		for (int i = 0; i < size; i++)
		{
			array[i] = this.array[size - i - 1];
		}
		return array;
	}

	private void ThrowForEmptyStack()
	{
		throw new InvalidOperationException();// (System.SR.InvalidOperation_EmptyStack);
	}


	internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
	{
		ICollection<T> collection = source as ICollection<T>;
		if (collection != null)
		{
			int count = collection.Count;
			if (count != 0)
			{
				T[] array = new T[count];
				collection.CopyTo(array, 0);
				length = count;
				return array;
			}
		}
		else
		{
			using IEnumerator<T> enumerator = source.GetEnumerator();
			if (enumerator.MoveNext())
			{
				T[] array2 = new T[4]
				{
					enumerator.Current,
					default(T),
					default(T),
					default(T)
				};
				int num = 1;
				while (enumerator.MoveNext())
				{
					if (num == array2.Length)
					{
						int num2 = num << 1;
						if ((uint)num2 > Array.MaxLength)
						{
							num2 = ((Array.MaxLength <= num) ? (num + 1) : Array.MaxLength);
						}
						Array.Resize(ref array2, num2);
					}
					array2[num++] = enumerator.Current;
				}
				length = num;
				return array2;
			}
		}
		length = 0;
		return Array.Empty<T>();
	}

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IValidationRule> GetEnumerator()
    {
		return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		return GetEnumerator();
    }

    internal struct Enumerator : IEnumerator<IValidationRule>, IDisposable, IEnumerator
	{
		private readonly int version;
		private readonly ValidationRuleStack stack;
		
		private int index;
		private IValidationRule current;

		public IValidationRule Current
		{
			get
			{
				if (index < 0)
				{
					ThrowEnumerationNotStartedOrEnded();
				}
				return this.current;
			}
		}

		object? IEnumerator.Current => Current;

		internal Enumerator(ValidationRuleStack stack)
		{
			this.stack = stack;
			this.version = stack.version;
			this.index = -2;
			this.current = default;
		}

		public void Dispose()
		{
			this.index = -1;
		}

		public bool MoveNext()
		{
			if (this.version != stack.version)
			{
				throw new InvalidOperationException("");// System.SR.InvalidOperation_EnumFailedVersion);
			}
			bool flag;
			if (this.index == -2)
			{
				this.index = stack.size - 1;
				flag = index >= 0;
				if (flag)
				{
					this.current = stack.array[index];
				}
				return flag;
			}
			if (index == -1)
			{
				return false;
			}
			flag = --index >= 0;
			if (flag)
			{
				this.current = stack.array[index];
			}
			else
			{
				this.current = default;
			}
			return flag;
		}

		private void ThrowEnumerationNotStartedOrEnded()
		{
			throw new InvalidOperationException("");// (_index == -2) ? System.SR.InvalidOperation_EnumNotStarted : System.SR.InvalidOperation_EnumEnded);
		}

		void IEnumerator.Reset()
		{
			if (version != stack.version)
			{
				throw new InvalidOperationException("");// System.SR.InvalidOperation_EnumFailedVersion);
			}
			index = -2;
			current = default;
		}
	}
}
