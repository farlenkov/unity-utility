using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UnityUtility
{
    public static class FindFileInParent
    {
        public static bool Exec(
            string fileName,
            out string filePath,
            string logTemplate = null)
        {
            return Exec(
                Application.dataPath,
                fileName,
                out filePath,
                logTemplate);
        }

        public static bool Exec(
            string folderPath,
            string fileName,
            out string filePath,
            string logTemplate = null)
        {
            if (folderPath == null)
            {
                filePath = null;
                return false;
            }

            if (!Directory.Exists(folderPath))
            {
                filePath = null;
                return false;
            }

            var possibleFilePath = Path.Combine(folderPath, fileName);
            var fileExists = File.Exists(possibleFilePath);

            if (logTemplate != null)
                Log.Info(logTemplate, possibleFilePath, fileExists);

            if (fileExists)
            {
                filePath = possibleFilePath;
                return true;
            }
            else
            {
                var parentPath = Path.GetDirectoryName(folderPath);

                if (parentPath != null)
                    return Exec(parentPath, fileName, out filePath, logTemplate);

                filePath = null;
                return false;
            }
        }
    }
}
