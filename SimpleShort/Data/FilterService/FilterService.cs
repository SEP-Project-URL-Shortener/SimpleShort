/*
 * FilterService:
 * Responsible for filtering bad words out of our program.
 * Checks the provided word and if the word is in the list, the URL will be blocked from being made.
 *
 * A highly consumable list of bad (profanity) English words based on the nice short and simple list found in Google's "what do you love" project made accessible by Jamie Wilkinson.
 * https://gist.github.com/jamiew
 * https://gist.github.com/jamiew/1112488
 */

// Included Libraries
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleShort.Data.FilterService
{
    public static class FilterService
    {
        // Check if the given word is not in the list of invalid words
        public static async Task<bool> IsValid(string word)
        {
            // cast the word to lowercase
            word = word.ToLower();

            // Get the list of invalid words from a local file
            var invalidWords = await File.ReadAllLinesAsync(@"./Data/FilterService/word.txt");

            // Check if the provided word contains any of the invalid words
            return invalidWords.Any(invalidWord => word.Contains(invalidWord));
        }
    }
}