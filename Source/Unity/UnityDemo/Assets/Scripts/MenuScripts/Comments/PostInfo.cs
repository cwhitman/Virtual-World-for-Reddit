using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedditSharp.Things;
using UnityEngine.UI;

public class PostInfo : VotableInfo {

	/// <summary>
	/// Creates the post.
	/// </summary>
	/// <param name="childDepth">Child depth.</param>
	/// <param name="post">Post.</param>
	public void Init(Post post)
	{
		this.thing = post;

		author.GetComponent<Text> ().text = post.AuthorName;
		time.GetComponent<Text> ().text = post.Created.ToString();
		upvotes.GetComponent<Text> ().text = "Upvotes:"+post.Upvotes;
		body.GetComponent<Text> ().text = post.SelfText;


		initializeButtons ();

		GameInfo.instance.redditRetriever.register (this);

	}

	protected override void initializeButtons()
	{
		replyButton.GetComponent<Button> ().onClick.AddListener (() => reply ());
		saveButton.GetComponent<Button> ().onClick.AddListener (() => toggleSave());
		saveButton.GetComponentInChildren<Text> ().text = thing.Saved ? "Unsave" : "Save";
		downvoteButton.GetComponent<Button> ().onClick.AddListener (() => downvote ());
		downvoteButton.GetComponentInChildren<Text> ().text = (thing.Vote == VotableThing.VoteType.Downvote) ? "Downvoted!" : "Downvote";
		upvoteButton.GetComponent<Button> ().onClick.AddListener (() => upvote ());
		upvoteButton.GetComponentInChildren<Text> ().text = (thing.Vote == VotableThing.VoteType.Upvote) ? "Upvoted!" : "Upvote";

		//no need for these buttons if the user isn't logged in.
		if (GameInfo.instance.reddit.User == null) {
			actionPanel.SetActive (false);
		}

	}

	public void reply()
	{
		GameInfo.instance.menuController.GetComponent<ReplyMenu> ().loadMenu ((Post)thing);
	}
}
