using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyHelper
{
	public static string ConvertToTycoonString(long _gold, long _minGoldCheck = 0)
	{
		if (_gold < 10)
		{
			return _gold.ToString();
		}
		if (_gold <= _minGoldCheck)
		{
			return string.Format("{0:0,0}", _gold);
		}
		// Debug.Log(">>>> " + _gold);
		string _result = "";
		if (_gold < 1000)
		{
			_result = _gold + "";
		}
		else if (_gold < 1000000)
		{
			_result = (_gold / 1000) + "";
			_gold = (_gold % 1000) / 10;
			if (_gold > 0)
				if (_gold > 9)
					if (_gold % 10 == 0)
						_result = _result + "." + (_gold / 10) + "K";
					else
						_result = _result + "." + _gold + "K";
				else
					_result = _result + "." + "0" + _gold + "K";
			else
				_result = _result + "K";
		}
		else if (_gold < 1000000000)
		{
			_result = (_gold / 1000000) + "";
			_gold = (_gold % 1000000) / 10000;
			if (_gold > 0)
				if (_gold > 9)
					if (_gold % 10 == 0)
						_result = _result + "." + (_gold / 10) + "M";
					else
						_result = _result + "." + _gold + "M";
				else
					_result = _result + "." + "0" + _gold + "M";
			else
				_result = _result + "M";
		}
		else if (_gold < 1000000000000)
		{
			_result = (_gold / 1000000000) + "";
			_gold = (_gold % 1000000000) / 10000000;
			if (_gold > 0)
				if (_gold > 9)
					if (_gold % 10 == 0)
						_result = _result + "." + (_gold / 10) + "B";
					else
						_result = _result + "." + _gold + "B";
				else
					_result = _result + "." + "0" + _gold + "B";
			else
				_result = _result + "B";
		}
		else if (_gold < 1000000000000000)
		{
			_result = (_gold / 1000000000000) + "";
			_gold = (_gold % 1000000000000) / 10000000000;
			if (_gold > 0)
				if (_gold > 9)
					if (_gold % 10 == 0)
						_result = _result + "." + (_gold / 10) + "T";
					else
						_result = _result + "." + _gold + "T";
				else
					_result = _result + "." + "0" + _gold + "T";
			else
				_result = _result + "T";
		}
		else
		{
			_result = (_gold / 1000000000000000) + "";
			_gold = (_gold % 1000000000000000) / 10000000000000;
			if (_gold > 0)
				if (_gold > 9)
					if (_gold % 10 == 0)
						_result = _result + "." + (_gold / 10) + "Q";
					else
						_result = _result + "." + _gold + "Q";
				else
					_result = _result + "." + "0" + _gold + "Q";
			else
				_result = _result + "Q";
		}
		return _result;
	}

	public static string ConvertToShortThousandString(int gold, bool forceLength_4)
	{
		int length = gold.ToString().Length;

		// Billion
		if (length >= 10)
		{
			if (length == 7 || length == 8)
			{
				string cal = gold.ToString().Substring(0, length - 9);
				char firstValue = gold.ToString()[cal.Length];

				return ConvertToThousandString(int.Parse(cal)) + (firstValue == 0 ? "" : ("," + firstValue)) + "B";
			}
			else
			{
				string cal = gold.ToString().Substring(0, length - 9);
				return ConvertToThousandString(int.Parse(cal)) + "B";
			}
		}
		// Million
		else if (length >= 7)
		{
			if (length == 7 || length == 8)
			{
				string cal = gold.ToString().Substring(0, length - 6);
				char firstValue = gold.ToString()[cal.Length];

				return ConvertToThousandString(int.Parse(cal)) + (firstValue == 0 ? "" : ("," + firstValue)) + "M";
			}
			else
			{
				string cal = gold.ToString().Substring(0, length - 6);
				return ConvertToThousandString(int.Parse(cal)) + "M";
			}
		}
		else if (length >= 5 && forceLength_4 == false)
		{
			string cal = gold.ToString().Substring(0, length - 3);
			return ConvertToThousandString(int.Parse(cal)) + "K";
		}
		else if (length >= 4 && forceLength_4 == true)
		{
			string cal = gold.ToString().Substring(0, length - 3);
			return ConvertToThousandString(int.Parse(cal)) + "K";
		}

		return ConvertToThousandString(gold);
	}

	public static string ConvertToThousandString(int value)
	{
		return value > 1000 ? string.Format("{0:N0}", value) : value.ToString();
	}
	public static string ConvertToThousandString(uint value)
	{
		return value > 1000 ? string.Format("{0:N0}", value) : value.ToString();
	}

	public static string ConvertWeiToString(string value)
	{
		return (decimal.Parse(value) / 1000000000000000000).ToString();
	}
}
