using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;
using FuzzyString;
using System.Data.SqlTypes;
using System.Collections;

/// <summary>
/// This class provides a simple interface to the methods available in the FuzzyString library.
/// You can find information about the original library at https://fuzzystring.codeplex.com/.
/// With the exception of the Jaro Winkler Prefix and boolean approximate match methods, all of
/// the methods in the class use the members of the class object to provide the parameters.
/// This was also done to make it easier to retrieve more of the distance metrics without having to
/// initialze objects multiple times.
/// </summary>

namespace SQLServerRegEx {

    public class StringDist {

        string from;
        string to;
        readonly StringDistOptions sdistOpts = new StringDistOptions();

        public StringDist(String from, String to) {
            this.from = from;
            this.to = to;
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double hamming() {
            return this.from.HammingDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double jaro() {
            return this.from.JaroDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double jaroWinkler() {
            return this.from.JaroWinklerDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double jaroWinklerPrefix(Double scale) {
            return this.from.JaroWinklerDistanceWithPrefixScale(this.to, scale);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double jaccard() {
            return this.from.JaccardDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double levenshtein() {
            return this.from.LevenshteinDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double levenshteinUpper() {
            return this.from.LevenshteinDistanceUpperBounds(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double levenshteinLower() {
            return this.from.LevenshteinDistanceLowerBounds(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double levenshteinNormalized() {
            return this.from.NormalizedLevenshteinDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double tanimotoCoefficient() {
            return this.from.TanimotoCoefficient(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double sorensenIndex() {
            return this.from.SorensenDiceIndex(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double sorensenDistance() {
            return this.from.SorensenDiceDistance(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double ratcliff() {
            return this.from.RatcliffObershelpSimilarity(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public Double overlap()  {
            return this.from.OverlapCoefficient(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public String longestSubString() {
            return this.from.LongestCommonSubstring(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public String longestSubSequence() {
            return this.from.LongestCommonSubsequence(this.to);
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public bool approxSame(String compOptions, String tolOptions) {
            var comparisonOptions = getComparisonOptions(compOptions);
            var toleranceOption = getToleranceOption(tolOptions);
            return this.from.ApproximatelyEquals(this.to, toleranceOption, comparisonOptions);
        }

        /*
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public AllDistanceMetrics allMetrics() {
            return new AllDistanceMetrics(hamming(), jaccard(), jaro(), jaroWinkler(),
                levenshtein(), levenshteinLower(), levenshteinUpper(), levenshteinNormalized(),
                overlap(), ratcliff(), sorensenIndex(), sorensenDistance(),
                tanimotoCoefficient());
        }
        */

        public FuzzyStringComparisonTolerance getToleranceOption(String opt) {
            var tolerances = sdistOpts.tol;
            return System.Text.RegularExpressions.Regex.IsMatch(opt, "[wsnm]") ? tolerances[opt] : tolerances["n"];
        }

        public FuzzyStringComparisonOptions[] getComparisonOptions(String options) {
            var optlist = sdistOpts.opts;
            var optionNames = sdistOpts.optnames;
            var opts = options.Split(',');
            FuzzyStringComparisonOptions[] parsedOptions = null;
            if (opts[0].ToLower() == "all") {
                parsedOptions = new FuzzyStringComparisonOptions[13];
                for (int i = 0; i < 13; i++) parsedOptions[i] = optlist[optionNames[i]];
            }
            else if (opts.Length > 1) {
                const List<FuzzyStringComparisonOptions> temp = null;
                for (int i = 0; i < opts.Length; i++) {
                    if (optlist.ContainsKey(opts[i])) temp.Add(optlist[opts[i]]);
                }
                temp.CopyTo(parsedOptions);
            }
            else if (opts.Length == 1) {
                if (optlist.ContainsKey(opts[0])) parsedOptions[0] = optlist[opts[0]];
            } else {
                parsedOptions[0] = optlist["jaroWinkler"];
            }
            return parsedOptions;
        }

    }

    /*
    public partial class AllDistanceMetrics {
        public Double Hamming_Distance;
        public Double Jaccard_Distance;
        public Double Jaro_Distance;
        public Double Jaro_Winkler_Distance;
        public Double Levenshtein_Distance;
        public Double Levenshtein_Distance_Lower_Bound;
        public Double Levenshtein_Distance_Upper_Bound;
        public Double Normalized_Levenshtein_Distance;
        public Double Overlap_Coefficient;
        public Double Ratcliff_Obershelp_Similarity;
        public Double Sorensen_Dice_Index;
        public Double Sorensen_Dice_Distance;
        public Double Tanimoto_Coefficient;

        public AllDistanceMetrics(Double h, Double jac, Double jaro, Double jarow,
            Double lev, Double levllow, Double levhigh, Double levnorm, Double overlap,
            Double ratcliff, Double diceIndex, Double diceDistance, Double tanimoto) {
            Hamming_Distance = h;
            Jaccard_Distance = jac;
            Jaro_Distance = jaro;
            Jaro_Winkler_Distance = jarow;
            Levenshtein_Distance = lev;
            Levenshtein_Distance_Lower_Bound = levllow;
            Levenshtein_Distance_Upper_Bound = levhigh;
            Normalized_Levenshtein_Distance = levnorm;
            Overlap_Coefficient = overlap;
            Ratcliff_Obershelp_Similarity = ratcliff;
            Sorensen_Dice_Index = diceIndex;
            Sorensen_Dice_Distance = diceDistance;
            Tanimoto_Coefficient = tanimoto;
        }

    }
    */

    public class StringDistOptions {

        public Dictionary<String, FuzzyStringComparisonTolerance> tol;
        public Dictionary<String, FuzzyStringComparisonOptions> opts;
        public String[] optnames;

        public StringDistOptions() {
            this.tol = setTolerances();
            this.opts = setOptions();
            this.optnames = getValidOptionNames();
        }

        private static Dictionary<String, FuzzyStringComparisonTolerance> setTolerances() {
            var tolerances = new Dictionary<String, FuzzyStringComparisonTolerance>();
            tolerances.Add("w", FuzzyStringComparisonTolerance.Weak);
            tolerances.Add("s", FuzzyStringComparisonTolerance.Strong);
            tolerances.Add("n", FuzzyStringComparisonTolerance.Normal);
            tolerances.Add("m", FuzzyStringComparisonTolerance.Manual);
            return tolerances;
        }

        public static Dictionary<String, FuzzyStringComparisonOptions> setOptions() {
            var optlist = new Dictionary<String, FuzzyStringComparisonOptions>();
            // Create object to look up distance metrics based on strings
            optlist.Add("hamming", FuzzyStringComparisonOptions.UseHammingDistance);
            optlist.Add("jaccard", FuzzyStringComparisonOptions.UseJaccardDistance);
            optlist.Add("jaro", FuzzyStringComparisonOptions.UseJaroDistance);
            optlist.Add("jaroWinkler", FuzzyStringComparisonOptions.UseJaroWinklerDistance);
            optlist.Add("levenshtein", FuzzyStringComparisonOptions.UseLevenshteinDistance);
            optlist.Add("subsequence", FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            optlist.Add("substring", FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            optlist.Add("normalizedLevenshtein", FuzzyStringComparisonOptions.UseNormalizedLevenshteinDistance);
            optlist.Add("overlap", FuzzyStringComparisonOptions.UseOverlapCoefficient);
            optlist.Add("ratcliffObershelp", FuzzyStringComparisonOptions.UseRatcliffObershelpSimilarity);
            optlist.Add("sorensenDiceDistance", FuzzyStringComparisonOptions.UseSorensenDiceDistance);
            optlist.Add("tanimotoCoefficient", FuzzyStringComparisonOptions.UseTanimotoCoefficient);
            optlist.Add("caseSensitive", FuzzyStringComparisonOptions.CaseSensitive);
            return optlist;

        }

        private static String[] getValidOptionNames() {
            var optionNames = new String[13];
            optionNames[0] = "hamming";
            optionNames[1] = "jaccard";
            optionNames[2] = "jaro";
            optionNames[3] = "jaroWinkler";
            optionNames[4] = "levenshtein";
            optionNames[5] = "subsequence";
            optionNames[6] = "substring";
            optionNames[7] = "normalizedLevenshtein";
            optionNames[8] = "overlap";
            optionNames[9] = "ratcliffObershelp";
            optionNames[10] = "sorensenDiceDistance";
            optionNames[11] = "tanimotoCoefficient";
            optionNames[12] = "caseSensitive";
            return optionNames;
        }

    }

}
