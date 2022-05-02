using System;
using System.Linq;
using System.Collections.Generic;

namespace Task_Scheduler
{
  class Program
  {
    static void Main(string[] args)
    {
      Solution s = new Solution();
      var tasks = new char[] { 'A', 'A', 'A', 'B', 'B', 'B' };
      var result = s.LeastInterval(tasks, 0);
      Console.Write(result);
    }
  }

  public class Solution
  {

    public class MaxComparer : IComparer<int>
    {
      public int Compare(int x, int y)
      {
        if (x < y)
        {
          return 1;
        }
        else if (x == y) return 0;
        else return -1;
      }
    }
    public int LeastInterval(char[] tasks, int n)
    {
      int tasksLength = tasks.Length;
      int[] map = new int[26];
      foreach (var c in tasks)
      {
        map[c - 'A']++;
      }
      Array.Sort(map);
      int max = map[25] - 1;
      int idleSlots = max * n; // Max repeated chars * Max possible idle time
      for (int i = 24; i >= 0; i--)
      {
        // Just fill with other chars by substracting from max possible idle time
        idleSlots -= Math.Min(map[i], max);
      }
      // if you can complete the tasks without idle time, just return the length of tasks
      return idleSlots > 0 ? tasksLength + idleSlots : tasksLength;
    }

    public int LeastInterval_Using_Dic(char[] tasks, int n)
    {
      int length = tasks.Length;
      Dictionary<char, int> frequency = new Dictionary<char, int>();
      foreach (char c in tasks)
      {
        if (!frequency.ContainsKey(c)) frequency.Add(c, 0);
        frequency[c] += 1;
      }

      var values = frequency.Values.OrderByDescending(x => x).ToList();

      int max = values[0] - 1;
      int noOfSlots = max * n;
      for (int i = 1; i < values.Count; i++)
      {
        int min = Math.Min(values[i], max);
        noOfSlots = noOfSlots - min;
      }

      return noOfSlots > 0 ? length + noOfSlots : length;
    }
  }
}
