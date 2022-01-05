﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MCDStorageChest.Extensions
{
    public static class Extensions
    {

        public static IEnumerable<T> dropLast<T>(this IEnumerable<T> enumerable, int numberToDropFromEnd)
        {
            var count = enumerable.Count();
            return enumerable.Take(count - numberToDropFromEnd);
        }

        public static IEnumerable<T> deepClone<T>(this IEnumerable<T> enumerable) where T:ICloneable
        {
            return enumerable.Select(element => element.Clone()).Cast<T>();
        }

        public static async Task<string> wgetAsync(string requestUriString)
        {
            try
            {
                var request = WebRequest.Create(requestUriString);
                using var response = await request.GetResponseAsync();
                using var dataStream = response.GetResponseStream();
                using var reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "{}";
        }

        public static string wget(string requestUriString)
        {
            var request = WebRequest.Create(requestUriString);
            using var response = request.GetResponse();
            using var dataStream = response.GetResponseStream();
            using var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            response.Close();
            return responseFromServer;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    if (child != null)
                    {
                        foreach (T childOfChild in FindVisualChildren<T>(child))
                            yield return childOfChild;
                    }

                }
            }
        }
    }
}
