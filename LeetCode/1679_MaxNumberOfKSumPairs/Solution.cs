using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Solution
    {
        public int MaxOperations(int[] nums, int k)
        {
            Array.Sort(nums);
            int first = 0;
            int last = nums.Length - 1;
            int count = 0;

            while (first < last)
            {
                if (nums[first] + nums[last] == k)
                {
                    first++;
                    last--;
                    count++;
                }

                if (nums[first] + nums[last] > k)
                    last--;

                if (nums[first] + nums[last] < k)
                    first++;

            }

            return count;

        }
    }
}
