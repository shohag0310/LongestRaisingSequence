using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQL.SystemTextJson; 
using System.Collections.Generic;

namespace longestRaisingSequence
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var schema = Schema.For(@"
                                type Query {
                                lis: String
                                            }
                                    ");


            int[] array = { 4, 6, -3, 3, 7, 9 };
            //int[] array = { 9, 6, 4, 5, 2, 0 };
            int arrayLength = array.Length;
            var result = ConstructLIS(array, arrayLength);
            var lisList = string.Join(", ", result);
            var json = await schema.ExecuteAsync(e =>
              {
                  e.Query = "{ lis }";
                  e.Root = new { lis = lisList };
              });

            Console.WriteLine(json);
        }

        static List<int> ConstructLIS(int[] array, int arrayLength)
        {

            List<List<int>> lis = new List<List<int>>();
            for (int i = 0; i < arrayLength; i++)
            {
                lis.Add(new List<int>());
            }

            lis[0].Add(array[0]);

            for (int i = 1; i < arrayLength; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if ((array[i] > array[j]) && (lis[i].Count < lis[j].Count + 1))
                    {
                        lis[i] = lis[j];
                    }
                }

                lis[i].Add(array[i]);
            }

            List<int> max = lis[0];
            foreach (List<int> item in lis)
            {
                if (item.Count > max.Count)
                {
                    max = item;
                }
            }

            return max;
        }
    }
}


