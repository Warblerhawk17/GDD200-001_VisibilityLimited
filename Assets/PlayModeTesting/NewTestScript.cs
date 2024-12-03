using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScript
{



    [UnityTest]
    public IEnumerator testPlayerAtributes()
    {
        SceneManager.LoadScene(1);
        yield return null;
        player_script player = GameObject.Find("Player").GetComponent<player_script>();
        Assert.IsNotNull(player);
        Assert.That(player.lives, Is.EqualTo(3));
        Assert.That(player.friendsSaved, Is.EqualTo(0));
        Assert.That(player.currentLightSource, Is.EqualTo(""));
        Assert.That(player.friendList.Count, Is.EqualTo(0));
    }

    [UnityTest]
    public IEnumerator testFriend()
    {
        SceneManager.LoadScene(1);
        yield return null;
        FriendFollow friend = GameObject.Find("Friend").GetComponent<FriendFollow>();
        Assert.IsNotNull(friend);
        Assert.AreEqual(friend.pickedUp, false);
        Assert.IsNull(friend.followTarget);
    }

    
}
