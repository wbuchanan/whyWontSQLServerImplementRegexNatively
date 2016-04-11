using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SqlTableTest {

        private class AllDistanceMetrics {

            public SqlString fromString;
            public SqlString toString;
            public SqlDouble Hamming_Distance;
            public SqlDouble Jaccard_Distance;
            public SqlDouble Jaro_Distance;
            public SqlDouble Jaro_Winkler_Distance;
            public SqlDouble Levenshtein_Distance;
            public SqlDouble Levenshtein_Distance_Lower_Bound;
            public SqlDouble Levenshtein_Distance_Upper_Bound;
            public SqlDouble Normalized_Levenshtein_Distance;
            public SqlDouble Overlap_Coefficient;
            public SqlDouble Ratcliff_Obershelp_Similarity;
            public SqlDouble Sorensen_Dice_Index;
            public SqlDouble Sorensen_Dice_Distance;
            public SqlDouble Tanimoto_Coefficient;

            public AllDistanceMetrics(String f, String t, Double h, Double jac, Double jaro, Double jarow,
                Double lev, Double levllow, Double levhigh, Double levnorm, Double overlap,
                Double ratcliff, Double diceIndex, Double diceDistance, Double tanimoto) {
                fromString = new SqlString(f);
                toString = new SqlString(t);
                Hamming_Distance = new SqlDouble(h);
                Jaccard_Distance = new SqlDouble(jac);
                Jaro_Distance = new SqlDouble(jaro);
                Jaro_Winkler_Distance = new SqlDouble(jarow);
                Levenshtein_Distance = new SqlDouble(lev);
                Levenshtein_Distance_Lower_Bound = new SqlDouble(levllow);
                Levenshtein_Distance_Upper_Bound = new SqlDouble(levhigh);
                Normalized_Levenshtein_Distance = new SqlDouble(levnorm);
                Overlap_Coefficient = new SqlDouble(overlap);
                Ratcliff_Obershelp_Similarity = new SqlDouble(ratcliff);
                Sorensen_Dice_Index = new SqlDouble(diceIndex);
                Sorensen_Dice_Distance = new SqlDouble(diceDistance);
                Tanimoto_Coefficient = new SqlDouble(tanimoto);
            }

        }


        [SqlFunction(
        DataAccess = DataAccessKind.Read,
        FillRowMethodName = "String_Distances_Filler",
        TableDefinition = "fromString NVARCHAR(MAX), toString NVARCHAR(MAX)")]
        public static IEnumerable GetStringDistances(String query) {

            ArrayList returnValues = new ArrayList();

            using (SqlConnection connection = new SqlConnection("context connection=true")) {

                connection.Open();

                using (SqlCommand dataTable = new SqlCommand(query, connection)) {

                    using (SqlDataReader row = dataTable.ExecuteReader()) {
                        while (row.Read()) {

                            CompStrings comparisonStrings = new CompStrings(row.GetSqlString(0), row.GetSqlString(1));
                            StringDist dists = new StringDist(comparisonStrings.from, comparisonStrings.to);
                            returnValues.Add(new AllDistanceMetrics(comparisonStrings.from, comparisonStrings.to, 
                                dists.hamming(), dists.jaccard(), dists.jaro(), dists.jaroWinkler(), 
                                dists.levenshtein(), dists.levenshteinLower(), dists.levenshteinUpper(),
                                dists.levenshteinNormalized(), dists.overlap(), dists.ratcliff(), dists.sorensenIndex(),
                                dists.sorensenDistance(), dists.tanimotoCoefficient()));
                        }
                    }
                }
            }
            return returnValues;
        }


        public static void String_Distances_Filler(object distances, 
            out SqlString fromString, out SqlString toString, out SqlDouble Hamming_Distance, 
            out SqlDouble Jaccard_Distance, out SqlDouble Jaro_Distance, out SqlDouble Jaro_Winkler_Distance, 
            out SqlDouble Levenshtein_Distance, out SqlDouble Levenshtein_Distance_Lower_Bound,
            out SqlDouble Levenshtein_Distance_Upper_Bound, out SqlDouble Normalized_Levenshtein_Distance,
            out SqlDouble Overlap_Coefficient, out SqlDouble Ratcliff_Obershelp_Similarity,
            out SqlDouble Sorensen_Dice_Index, out SqlDouble Sorensen_Dice_Distance, 
            out SqlDouble Tanimoto_Coefficient) {

            AllDistanceMetrics adm = (AllDistanceMetrics)distances;
            fromString = adm.fromString;
            toString = adm.toString;
            Hamming_Distance = adm.Hamming_Distance;
            Jaccard_Distance = adm.Jaccard_Distance;
            Jaro_Distance = adm.Jaro_Distance;
            Jaro_Winkler_Distance = adm.Jaro_Winkler_Distance;
            Levenshtein_Distance = adm.Levenshtein_Distance;
            Levenshtein_Distance_Lower_Bound = adm.Levenshtein_Distance_Lower_Bound;
            Levenshtein_Distance_Upper_Bound = adm.Levenshtein_Distance_Upper_Bound;
            Normalized_Levenshtein_Distance = adm.Normalized_Levenshtein_Distance;
            Overlap_Coefficient = adm.Overlap_Coefficient;
            Ratcliff_Obershelp_Similarity = adm.Ratcliff_Obershelp_Similarity;
            Sorensen_Dice_Index = adm.Sorensen_Dice_Index;
            Sorensen_Dice_Distance = adm.Sorensen_Dice_Distance;
            Tanimoto_Coefficient = adm.Tanimoto_Coefficient;
        }
    }

}
