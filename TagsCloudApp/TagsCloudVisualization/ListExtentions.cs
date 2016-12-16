using System.Collections.Generic;

namespace TagsCloudVisualization
{
    internal static class ListExtentions
    {
        public static int FindFirstBiggerOrEqualIndex(this List<int> ascendingSortedList, int value)
        {
            // binary search here
            // there is standard, but it doesn't do what i want
            var leftIndex = -1;
            var rightIndex = ascendingSortedList.Count;
            while (rightIndex - leftIndex != 1)
            {
                var midIndex = (leftIndex + rightIndex) / 2;
                if (ascendingSortedList[midIndex] < value)
                    leftIndex = midIndex;
                else
                    rightIndex = midIndex;
            }
            return rightIndex;
        }
    }
}
