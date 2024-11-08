using System.Collections;
using UnityEngine;
using System.IO;
using System;

public static class MonoHelper
{
	#region Graphics
	/// <summary>
	/// Textures the circle mask.
	/// </summary>
	/// <returns>The circle mask.</returns>
	/// <param name="h">The height.</param>
	/// <param name="w">The width.</param>
	/// <param name="r">The red component.</param>
	/// <param name="cx">Cx.</param>
	/// <param name="cy">Cy.</param>
	/// <param name="sourceTex">Source tex.</param>
	public static Texture2D TextureCircleMask(int h, int w, float r, float cx, float cy, Texture2D sourceTex)
	{
		Color[] c = sourceTex.GetPixels(0, 0, sourceTex.width, sourceTex.height);
		Texture2D b = new Texture2D(h, w);
		for (int i = 0; i < (h * w); i++)
		{
			int y = Mathf.FloorToInt(((float)i) / ((float)w));
			int x = Mathf.FloorToInt(((float)i - ((float)(y * w))));
			if (r * r >= (x - cx) * (x - cx) + (y - cy) * (y - cy))
			{
				b.SetPixel(x, y, c[i]);
			}
			else
			{
				b.SetPixel(x, y, Color.clear);
			}
		}
		b.Apply();
		return b;
	}
	#endregion

	#region Code Mechanic
	public static IEnumerator DoSomeThing(float seconds, UnityEngine.Events.UnityAction callback)
	{
		yield return new WaitForSeconds(seconds);

		if (callback != null)
			callback.Invoke();
	}

	public static IEnumerator DoSomeThingAfterFrame(int frameWait, UnityEngine.Events.UnityAction callback)
	{
		for (int i = 0; i < frameWait; i++)
			yield return null;

		if (callback != null)
			callback.Invoke();
	}
	#endregion

	public static IEnumerator DownloadTexture(string url, Action<Texture2D> callback)
	{
		// Start a download of the given URL
		using (WWW www = new WWW(url))
		{
			// Wait for download to complete
			yield return www;

			callback(www.texture);
		}
	}

	public static string ConvertSecondToTime(int second, bool exactly = false)
	{
		string answer = "";
		TimeSpan t = TimeSpan.FromSeconds(second);
		long a = second / 60;
		if (a >= 60)
		{
			if (t.Seconds == 0 && t.Minutes == 0 && exactly)
			{
				answer = string.Format("{0:D1}h",
				(long)t.TotalHours
				);
			}

			else if (t.Seconds == 0 && exactly)
			{
				answer = string.Format("{0:D1}h {1:D2}m",
				(long)t.TotalHours,
				(long)t.Minutes
				);
			}
			else
			{
				answer = string.Format("{0:D1}h {1:D2}m {2:D2}s",
				(long)t.TotalHours,
				t.Minutes,
				t.Seconds
				);
			}
		}
		else if (a >= 1)
		{
			answer = string.Format("{0:D2}m {1:D2}s",
		   t.Minutes,
		   t.Seconds
		   );
		}
		else
		{
			answer = string.Format("{0:D2}s",
			t.Seconds);

		}
		return answer;
	}

	public static string ConvertLargeNumber(long number)
	{
		if (number < 1000000 && number > 1000)
		{
			return string.Format("{0}K", Mathf.FloorToInt(number / 1000));
		}
		else if (number > 1000000)
		{
			return string.Format("{0}M", Mathf.FloorToInt(number / 1000000));
		}
		return "";
	}

	public static Texture2D LoadPNG(string filePath)
	{

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}

	// First launch
	public static bool IsFirstLaunch(string name)
	{
		return (PlayerPrefs.GetInt(name + "_first_launch_pref_string", 1) == 1) ? true : false;
	}

	public static void SetIsFirstLaunchDone(string name)
	{
		PlayerPrefs.SetInt(name + "_first_launch_pref_string", 0);
	}

	public static IEnumerator DownloadFileCoroutine(string url, Action<string> successCallback, Action<string> failCallback)
	{
		WWW www = new WWW(url);

		yield return www;

		if (www.error != null)
		{
			if (failCallback != null)
				failCallback.Invoke(www.error);
		}
		else if (www.bytesDownloaded == 0) // Cheat as error
		{
			if (failCallback != null)
				failCallback.Invoke("(DownloadFileCoroutine) File empty!");
		}
		else
		{
			if (successCallback != null)
				successCallback(www.text);
		}
	}

	public static Stream GenerateStreamFromString(string s)
	{
		var stream = new MemoryStream();
		var writer = new StreamWriter(stream);
		writer.Write(s);
		writer.Flush();
		stream.Position = 0;
		return stream;
	}


	public static string ConvertByteToBase64(byte[] bytes)
	{
		return System.Convert.ToBase64String(bytes);
	}
	public static byte[] ConvertBase64ToByte(string base64str)
	{
		return System.Convert.FromBase64String(base64str);
	}
}