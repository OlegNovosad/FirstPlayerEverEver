using System;

public class Constants
{
	public const string FlowerMessage = "Just a flower";
	public const string FireMessage = "Its hot. Like me.";
	public const string SignMessage = "Hmm...";

	public enum Skill
	{
		None,
		ThrowSpear,
		PullOutSpear
	}

	public enum QuestState
	{
		None,
		Started,
		InProgress,
		Done
	}

	public enum Item
	{
		Garlic,
		Crusifix,
		Stake
	}

	public const int MaxPlayerHealth = 42;
}