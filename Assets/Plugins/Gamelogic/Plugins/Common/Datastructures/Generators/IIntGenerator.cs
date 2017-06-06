﻿namespace Gamelogic
{
	/// <summary>
	/// A generator that generates integers. IIntGenerators are often used
	///	to generate random elements from lists or arrays, where the ints 
	/// generated are used to index into the list or array.
	/// </summary>
	public interface IIntGenerator : IGenerator<int>
	{
	}
}