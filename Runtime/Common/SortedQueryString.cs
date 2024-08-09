using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using UnityEngine;

namespace UnityUtility
{
    public static class SortedQueryString
    {
        public static string Sort(
            NameValueCollection data, 
            bool includeNames,
            string separator, 
            string excludeKey)
        {
            var dataDict = new SortedDictionary<string, string>(
                data.AllKeys.ToDictionary(x => x!, x => data[x]!),
                StringComparer.Ordinal);

            return string.Join(
                separator, 
                dataDict.Where(x => x.Key != excludeKey)
                .Select(x => includeNames 
                    ? $"{x.Key}={x.Value}"
                    : x.Value));
        }
    }
}