using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    /// Problem 1 - Find Pairs with Sets (O(n))
    
    
    /// Problem 1 - Find Pairs with Sets (O(n))
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>(words);
        var results = new List<string>();
        var used = new HashSet<string>();

        foreach (var word in words)
        {
            // Skip palindromes like "aa" — they can't have a symmetric pair
            if (word[0] == word[1]) continue;

            var reversed = $"{word[1]}{word[0]}";

            // Only add if the reverse exists and we haven't already added this pair
            if (seen.Contains(reversed) && !used.Contains(reversed))
            {
                results.Add($"{word} & {reversed}");
                used.Add(word); // mark current word so we don't add it again from the other side
            }
        }

        return results.ToArray();
    }

    /// Problem 2 - Degree Summary
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length < 5) continue;

            var degree = fields[4].Trim();

            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    /// Problem 3 - Anagram Check
    public static bool IsAnagram(string word1, string word2)
    {
        // Normalize: lowercase and remove spaces
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // Different lengths after normalization means they can't be anagrams
        if (word1.Length != word2.Length) return false;

        var letterCount = new Dictionary<char, int>();

        // Count letters in word1
        foreach (var ch in word1)
        {
            if (letterCount.ContainsKey(ch))
                letterCount[ch]++;
            else
                letterCount[ch] = 1;
        }

        // Subtract counts using word2
        foreach (var ch in word2)
        {
            if (!letterCount.ContainsKey(ch)) return false;
            letterCount[ch]--;
            if (letterCount[ch] < 0) return false;
        }

        // All counts must be zero
        return letterCount.Values.All(count => count == 0);
    }

    /// Problem 5 - Earthquake Daily Summary
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        return featureCollection?.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray() ?? [];
    }
}