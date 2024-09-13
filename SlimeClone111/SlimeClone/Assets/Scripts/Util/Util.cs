using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Linq;
using System.Text;
using TMPro;
using System.Reflection;

using UnityEditor;

public static class Util
{
    public static void Log<T>(string text, T num)//���� �α�.
    {
        Debug.Log(text + " is " + num);
    }
    public static int SumArray(int[] array)//�迭�� ���հ�
    {
        int n = 0;
        for (int i = 0; i < array.Length; i++)
            n += array[i];
        return n;
    }
    public static float SumArray(float[] array)//�迭�� ���հ�
    {
        float n = 0;
        for (int i = 0; i < array.Length; i++)
            n += array[i];
        return n;
    }
    public static string FormatComma(long number)
    {
        return string.Format("{0:#,##0}", number);
    }
    public static string FormatComma(int number)
    {
        return string.Format("{0:#,##0}", number);
    }
    public static string FormatComma(double value, int decimalPlaces)
    {
        return string.Format("{0:F" + decimalPlaces + "}", value);
    }

    public static string FormatComma(float value, int decimalPlaces)
    {
        return string.Format("{0:F" + decimalPlaces + "}", value);
    }
    public static void InitArray<T>(int[] array, int resetnumber)
    {
        for (int i = 0; i < array.Length; i++)
            array[i] = resetnumber;
    }
    public static float Round(float num, int n)//n��° ���ڱ��� �ø�.
    {
        return (float)Math.Round(num, n);
    }
    public static float Round(double num, int n)//n��° ���ڱ��� �ø�.
    {
        return (float)Math.Round(num, n);
    }
    public static bool Probability(double chance)
    {
        return (UnityEngine.Random.value <= chance);
    }
    public static bool Probability(float chance)
    {
        return (UnityEngine.Random.value <= chance);
    }
    public static int GetWeightedRandomIndex(params float[] pers)//Ȯ���� ���ʴ��?���� 1�̵Ǵ°��� ����) �־��� �� �ش��ϴ� Ȯ���� �ɸ��� 01234�� ���ʷ� ����.
    {
        int maxLenth = 0;

        if (pers.Length == 1)//�迭���̰� 1�̸� ������ �� ���� ���;������� 0�� �����Ѵ�.
            return 0;

        //1.���� �Ҽ����� �� ������ ã�Ƴ���.
        int lenth;
        decimal total = 0;
        for (int i = 0; i < pers.Length; i++)
        {
            lenth = pers[i].ToString().Substring(pers[i].ToString().IndexOf('.') + 1).Length;

            total += (decimal)pers[i];
            if (maxLenth < lenth)
                maxLenth = lenth;
        }

        int correction = (int)(total * (decimal)Math.Pow(10, maxLenth)); //-> �����ִ� �� 

        int randomNumber = UnityEngine.Random.Range(1, correction + 1);
        int tempNum = 0;
        int num = 0;
        for (int i = 0; i < pers.Length; i++)
        {
            num = (int)(correction * (decimal)pers[i]);
            if (num + tempNum >= randomNumber)
            {
                //Debug.Log(num + tempNum + ">=" + randomNumber);
                return i;
            }
            tempNum += num;
        }
        Debug.Log(total + "-" + maxLenth + "-" + randomNumber + "-" + tempNum + "-" + num);
        Debug.Log("ERROR : ���������� ���� ����." + correction);
        return 0;
    }
    public static int GetRandomIndex(params double[] probabilities)
    {
        double totalProbability = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            if (probabilities[i] < 0 || probabilities[i] > 1)
            {
                throw new ArgumentOutOfRangeException($"Probability at index {i} is out of range (0~1)");
            }

            totalProbability += probabilities[i];
        }

        if (totalProbability < 0 || totalProbability > 1)
        {
            throw new ArgumentException("Sum of probabilities is out of range (0~1)");
        }

        double randomValue = new Random().NextDouble();

        double probabilitySum = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilitySum += probabilities[i];

            if (randomValue < probabilitySum)
            {
                return i;
            }
        }
        return probabilities.Length - 1;
    }

    public static string Pertext(float num)
    {
        return Math.Round((num) * 100, 2) + "%";
    }
    public static string Pertext(double num)
    {
        return Math.Round((num) * 100, 2) + "%";
    }
    public static string Pertext(int num)
    {
        return num.ToString();
    }
    public static T RandomArray<T>(T[] per)
    {
        if (per.Length == 0)
            return default(T);

        int n = UnityEngine.Random.Range(0, per.Length);
        return per[n];
    }
    public static T RandomParamsArray<T>(params T[] per)
    {
        int n = UnityEngine.Random.Range(0, per.Length);
        return per[n];
    }
    public static T RandomList<T>(List<T> per)
    {
        int n = UnityEngine.Random.Range(0, per.Count);
        return per[n];
    }

    public static string TimeFormat(double timeValue)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeValue);
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        int seconds = timeSpan.Seconds;

        if (timeValue < 60) // 60�� �̸�
        {
            return seconds.ToString("00");
        }
        else if (timeValue < 3600) // 1�ð� �̸�
        {
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else // 1�ð� �̻�
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }
    public static bool EqualsVector2D(Vector2 vector_1, Vector2 vector_2, float errorrange) //��ǥ�� ������ 1 �̸��� ���?
    {
        if (Mathf.Abs(vector_1.x - vector_2.x) < errorrange && Mathf.Abs(vector_1.y - vector_2.y) < errorrange)
            return true;
        return false;
    }
    public static bool EqualsVector2D(Vector2 vector_1, Vector2 vector_2) //��ǥ�� ������ 1 �̸��� ���?
    {
        return EqualsVector2D(vector_1, vector_2, 1);
    }

    #region Max~Min
    public static int Max(int num, int max)
    {
        if (num > max)
            return max;
        return num;
    }
    public static float Max(float num, float max)
    {
        if (num > max)
            return max;
        return num;
    }
    public static long Max(long num, long max)
    {
        if (num > max)
            return max;
        return num;
    }
    public static int Min(int num, int min)
    {
        if (num < min)
            return min;
        return num;
    }
    public static float Min(float num, float min)
    {
        if (num < min)
            return min;
        return num;
    }


    public static long Min(long num, long min)
    {
        if (num < min)
            return min;
        return num;
    }
    public static int MinMax(int num, int min, int max)
    {
        if (num < min)
            return min;
        if (num > max)
            return max;
        return num;
    }
    public static float MinMax(float num, float min, float max)
    {
        if (num < min)
            return min;
        if (num > max)
            return max;
        return num;
    }
    public static long MinMax(long num, long min, long max)
    {
        if (num < min)
            return min;
        if (num > max)
            return max;
        return num;
    }
    public static bool SequenceEqual<T>(List<T> list1, List<T> list2)
    {
        return Enumerable.SequenceEqual(
                   list1.OrderBy(a => a), list2.OrderBy(a => a));
    }

    public static long ExtraExcelConvert(string text)
    //�������� �ڸ��� ���� ���� �ε��?���?
    {
        if (text.Contains('.'))
        {
            Debug.LogException(new Exception("Input number is not decimal. Input :" + text));
            return -1;
        }

        if (text.Contains("E"))
            return (long)(Double.Parse(text.Substring(0, text.IndexOf("+") - 1)) * Math.Pow(10, Int64.Parse(text.Substring(text.IndexOf("+") + 1))));
        return Int64.Parse(text);
    }
    public static List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }
    public static T[] Shuffle<T>(T[] list)
    {
        for (int i = list.Length - 1; i > 0; i--)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }

    public static string PrintArray<T>(T[] arrays)
    {
        StringBuilder stringReader = new StringBuilder();
        foreach (var item in arrays)
        {
            stringReader.Append(item.ToString() + ", ");
        }
        return stringReader.ToString();
    }

    public static int MostFrequent(int[] arr)
    {
        // Sort the array
        Array.Sort(arr);

        // find the max frequency using
        // linear traversal
        int max_count = 1, res = arr[0];
        int curr_count = 1;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] == arr[i - 1])
                curr_count++;
            else
                curr_count = 1;

            // If last element is most frequent
            if (curr_count > max_count)
            {
                max_count = curr_count;
                res = arr[i - 1];
            }
        }

        return res;
    }
    #endregion

    [System.Serializable]
    public struct UIObjects
    {
        public string ID;
        public GameObject gameObject;
    }
    public struct UIObjectsAction
    {
        public UIObjectsAction(string ID, Action action)
        {
            this.ID = ID;
            this.action = action;
        }

        public string ID;
        public Action action;
    }
    public static void SetUIController(UIObjects[] uiObject, string command, params UIObjectsAction[] uIObjectsActions)
    {
        string[] str = command.Split('_');
        if (str.Length != 2)
            return;
        UIObjects selectedObj = new UIObjects();
        foreach (var obj in uiObject)
        {
            if (obj.ID.Equals(str[0]))
            {
                selectedObj = obj; break;
            }
        }

        if (selectedObj.ID == null || selectedObj.ID == "")
            return;

        if (str[1] == "On")
        {
            selectedObj.gameObject.SetActive(true);
        }
        else if (str[1] == "Off")
        {
            selectedObj.gameObject.SetActive(false);
        }

        foreach (var obj in uIObjectsActions)
        {
            if (obj.ID.Equals(command))
            {
                obj.action();
            }
        }
    }
    public static int ClampNumber(int value, int min, int max)
    {
        return value < min ? min : (value >= max ? max : value);
    }

    public static T ToEnum<T>(string str, T basic) where T : Enum
    {
        foreach (T value in Enum.GetValues(typeof(T)))
        {
            if (value.ToString().Equals(str.Trim()))
            {
                return value;
            }
        }

        Debug.LogError("Error : invalid input for converting str to SpecialAbilityType : " + str);
        return basic;
    }

    //Ȯ�� �Լ�, (1,1) - (3,2) - (6,3), �� �Է½� ���� 10% Ȯ���� 1, 30%Ȯ���� 2, 60% Ȯ���� 3�� return.
    //��, �տ� �ִ� ������ ���߸�ŭ Ȯ���� ������. �ش��ϴ� Ȯ����, �ڿ� �ִ� ���� �� ��ȯ.
    public static T RandomTuple<T>(List<Tuple<int, T>> tuples)
    {
        int sum = 0;
        foreach (var value in tuples)
        {
            sum += value.Item1;
        }
        int pickValue = UnityEngine.Random.Range(0, sum + 1);
        int tmpNum = 0;
        foreach (var value in tuples)
        {
            tmpNum += value.Item1;
            if (pickValue < tmpNum + value.Item1)
            {
                return value.Item2;
            }
        }
        return tuples[0].Item2;
    }

    public static float CalculateAngle(Vector2 currentObjectPosition, Vector2 targetObjectPosition)
    {
        // �� �� ���� ���̸� ���?
        Vector2 direction = targetObjectPosition - currentObjectPosition;

        // Atan2 �Լ��� ����Ͽ�?��ũź��Ʈ ���� ���?
        float radians = Mathf.Atan2(direction.x, direction.y);

        // ���� ���� ������ ��ȯ
        float degrees = radians * Mathf.Rad2Deg;

        // ���?������ ������ ���?360�� ���Ͽ� �����?��ȯ
        if (degrees < 0)
        {
            degrees += 360;
        }
        return degrees;
    }

    public static void DebugDictionary<TKey, TValue>(Dictionary<TKey, TValue> dic)
    {
        string tmpStr = "";

        foreach (var it in dic)
        {
            tmpStr += "Key :" + it.Key.ToString() + " , Value :" + it.Value.ToString() + "\n";
        }

        Debug.Log(tmpStr);

    }

    public static void DebugTupleArray<TKey, TValue>(Tuple<TKey, TValue>[] tup)
    {
        string tmpStr = "";

        foreach (var it in tup)
        {
            tmpStr += "Item1 :" + it.Item1.ToString() + " , Item2 :" + it.Item2.ToString() + "\n";
        }

        Debug.Log(tmpStr);
    }

    public static void DebugTupleArray<TKey, TValue>(string DebugName, Tuple<TKey, TValue>[] tup)
    {
        string tmpStr = "";

        foreach (var it in tup)
        {
            tmpStr += "Item1 :" + it.Item1.ToString() + " , Item2 :" + it.Item2.ToString() + "\n";
        }

        Debug.Log(DebugName + " : " + tmpStr);
    }

    public static void DebugList<T>(List<T> list)
    {
        string tempStr = "";
        int i = 0;
        foreach (var it in list)
        {
            tempStr += "No." + i + " Item : " + it;
            i++;
        }

        Debug.Log(tempStr);
    }

    public static double NextGaussian(double mean, double stdDev)
    {
        Random random = new Random();
        double u1 = 1.0 - random.NextDouble();
        double u2 = 1.0 - random.NextDouble();
        double normalValue = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        return mean + stdDev * normalValue;
    }

    public static string RemoveComments(string input)
    {
        int startIndex = input.IndexOf("/*");
        if (startIndex != -1)
        {
            int endIndex = input.IndexOf("*/", startIndex);
            if (endIndex != -1)
            {
                input = input.Remove(startIndex, endIndex - startIndex + 2);
            }
        }
        return input.Trim();
    }

    #region
    public static void SetDictionary<T>(ref Dictionary<T, double> dictionary, T key, double value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] += value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public static void SetDictionary<T>(ref Dictionary<T, float> dictionary, T key, float value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] += value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }
    public static void SetDictionary<T>(ref Dictionary<T, int> dictionary, T key, int value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] += value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }
    public static void SetDictionary<T>(ref Dictionary<T, long> dictionary, T key, long value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] += value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    #endregion

    public static IJsonSerializable[] ToDataArray<Original, IJsonSerializable>(Original[] originals, Func<Original, IJsonSerializable> toData) //where DataType : IJsonSerializable<Original, DataType>
    {
        IJsonSerializable[] data = new IJsonSerializable[originals.Length];
        for (int i = 0; i < originals.Length; i++)
        {
            if (originals[i] == null)
            {
                continue;
            }
            data[i] = toData(originals[i]);
        }
        return data;
    }
    public static Original[] ToOriginalArray<IJsonSerializable, Original>(IJsonSerializable[] datas, Func<IJsonSerializable, Original> toOriginal) //where DataType : IJsonSerializable<Original, DataType>
    {
        var originals = new Original[datas.Length];
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] == null)
                continue;
            originals[i] = toOriginal(datas[i]);
        }
        return originals;
    }

    public static void TextSizeController(TMP_Text text, float MaxSizePer, float time)
    {
        if (!text.GetComponent<MonoBehaviour>().IsInvoking("ChangeTextSize"))
            text.StartCoroutine(ChangeTextSize(text, true, MaxSizePer, 0, time));
    }

    static IEnumerator ChangeTextSize(TMP_Text text, bool isGrowing, float MaxSizePer, float elapsedTime, float time)
    {
        var tmpSize = text.fontSize;
        while (elapsedTime < time)
        {
            float newSize = Mathf.Lerp(1f, MaxSizePer, elapsedTime / (time / 2f));

            if (!isGrowing)
            {
                newSize = Mathf.Lerp(MaxSizePer, 1f, (elapsedTime - time / 2f) / (time / 2f));
            }

            text.fontSize = tmpSize * newSize;

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= time / 2f && isGrowing)
            {
                isGrowing = false; // ũ�� ������ �������� ũ�� ���ҷ� ��ȯ
            }

            yield return null;
        }
        text.fontSize = tmpSize;
    }


    public static double[] Distribution(double[] original, int dis_cnt, double per)
    {
        if (dis_cnt < 0 || dis_cnt >= original.Length)
        {
            throw new ArgumentException("Invalid distribution count.");
        }

        double[] tmpArray = new double[original.Length];
        double total = 0;

        // Calculate the total sum of the array
        for (int i = 0; i < original.Length; i++)
        {
            total += original[i];
        }

        // Distribute the specified percentage from the first dis_cnt elements
        double distributedSum = 0;
        for (int i = 0; i < dis_cnt; i++)
        {
            double distributedValue = original[i] * per;
            tmpArray[i] = original[i] - distributedValue;
            distributedSum += distributedValue;
        }

        // Distribute the remaining sum proportionally to the elements after dis_cnt
        double remainingSum = total - distributedSum;
        for (int i = dis_cnt; i < tmpArray.Length; i++)
        {
            tmpArray[i] = original[i] + tmpArray[i] * distributedSum / remainingSum;
        }

        return tmpArray;
    }

    public static string ReplaceValue(string str, double value, bool isPercentage)
    {
        double tmpValue = isPercentage ? value * 100 : value;
        // Check if the string contains the placeholder %d
        if (str.Contains("%d"))
        {
            // Replace %d with the formatted value"<color=#0000ff>" +  + "</color>"
            var it = isPercentage ? Math.Round(value * 100, 2) + "%" : Round(value, 2).ToString();
            str = str.Replace("%d", "<color=#18FF00>" + it + "</color>");
        }

        return str;
    }

    public static int RandomCount(int count, float prob)
    {
        int cnt = 0;
        for (int i = 0; i < count; i++)
        {
            if (Probability(prob))
                cnt++;
        }
        return cnt;
    }

    public static string AbbreviateNumber(long number)
    {
        string result = number.ToString();
        char finals = ' ';
        if (number >= 1_000_000_000)
        {
            result = $"{(double)number / 1_000_000_000:F2}";
            finals = 'B';
        }
        else if (number >= 1_000_000)
        {
            result = $"{(double)number / 1_000_000:F2}";
            finals = 'M';
        }
        else if (number >= 1_000)
        {
            result = $"{(double)number / 1_000:F2}";
            finals = 'K';
        }

        // Remove trailing ".00" if exists
        result = result.EndsWith(".00") ? result.Substring(0, result.Length - 3) : result;

        return result + finals;
    }


    public static string AbbreviateNumber(int number)
    {
        return AbbreviateNumber((long)number);
    }
    public static bool IsNumericType(Type type)
    {
        return type.IsPrimitive && type != typeof(char) && type != typeof(bool);
    }

    public static string CurrentDayString(DateTimeOffset dateTime)
    {
        return "[" + dateTime.Year + "," + dateTime.Month + "," + dateTime.Day + "]";
    }
    public static string CurrentDayString(DateTimeOffset? dateTime)
    {
        if (dateTime == null)
            return "Envaild Time Value [" + DateTimeOffset.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "]";
        return "[" + dateTime?.Year + "," + dateTime?.Month + "," + dateTime?.Day + "]";
    }

  /*  public static bool IsJson(string obj)
    {
        try
        {
            JsonConvert.DeserializeObject(obj);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }

    }*/
}
