// ***********************************************************************
// Assembly         : ENSEK,Entities
// Author           : pdurr
// Created          : 04-12-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="ConvertToJson.cs" company="ENSEK,Entities">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Text;

namespace ENSEK.Entities.Extensions;

/// <summary>
///     Class ConvertToJson.
/// </summary>
public class ConvertToJson
{
    public const char unitString = '{';
    public const char unitStringStart = '[';
    public const char unitStringEnd = '[';

    /// <summary>
    ///     Converts the CSV file to json object.
    /// </summary>
    /// <param name="readerInput">The reader input.</param>
    /// <returns>System.String.</returns>
    public static string ConvertCsvFileToJsonObject(Stream readerInput)
    {
        var json = string.Empty;
        var csv = string.Empty;


        readerInput.Position = 0;


        using (var readercsv = new StreamReader(readerInput, Encoding.UTF8))
        {
            csv = readercsv.ReadToEnd();
        }

        var lines = csv.Split(new[] { "\n" }, StringSplitOptions.None);


        if (lines.Length > 1)
        {
            // parse headers
            var headers = lines[0].Split(',');

            var sbjson = new StringBuilder();
            sbjson.Clear();
            sbjson.Append(unitStringStart);

            // parse data
            for (var i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                if (string.IsNullOrEmpty(lines[i])) continue;

                sbjson.Append(unitString);

                var data = lines[i].Split(',');

                for (var h = 0; h < headers.Length; h++)
                    sbjson.Append(
                        $"\"{headers[h]}\": \"{data[h]}\"" + (h < headers.Length - 1 ? "," : null)
                    );

                sbjson.Append("}" + (i < lines.Length - 1 ? "," : null));
            }

            sbjson.Append(unitStringEnd);

            json = sbjson.ToString();
        }

        return json;
    }
}