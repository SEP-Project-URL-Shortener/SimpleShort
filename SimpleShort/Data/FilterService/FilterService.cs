/*
 * FilterService:
 * Responsible for filtering bad words out of our program.
 * Checks the provided word and if the word is in the list, the URL will be blocked from being made.
 *
 * A highly consumable list of bad (profanity) English words based on the nice short and simple list found in Google's "what do you love" project made accessible by Jamie Wilkinson.
 * https://gist.github.com/jamiew
 * https://gist.github.com/jamiew/1112488
 */

using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SimpleShort.Data.FilterService
{
    public static class FilterService
    {
        public static async Task<bool> IsValid(string word)
        {
            word = word.ToLower();
            var invalidWords = await File.ReadAllLinesAsync(@"./Data/FilterService/word.txt");
            return invalidWords.Any(invalidWord => word.Contains(invalidWord));
        }
    }
}
