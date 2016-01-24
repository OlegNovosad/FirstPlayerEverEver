using System;

public class Constants
{
	public const string FlowerMessage = "Just a flower";

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

	public const int MaxPlayerHealth = 42;
	public const int FlowersCount = 17;
}