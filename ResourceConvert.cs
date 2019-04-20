using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMRando {
    public static class ResourceConvert {
        // Converts the item ids in a logic string into a string with the names.
        // Use this to make logic files human readable.
        public static string ConvertLogicToNames(string logic) {
            string[] lines = logic.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            StringBuilder newOutput = new StringBuilder();

            // Remove comments
            List<string> nonCommentLines = new List<string>();
            for(int i = 0; i < lines.Length; i++) {
                if(!lines[i].Contains('-')) {
                    nonCommentLines.Add(lines[i]);
                }
            }
            lines = nonCommentLines.ToArray();

            for(int i = 0, itemID = 0; i < lines.Length; itemID++) {
                // Print Name
                newOutput.AppendLine("-" + itemID + ": " + mmrMain.ItemDataToName(itemID.ToString()));

                // Print Dependancies
                newOutput.AppendLine(ConvertCommaSeperatedItemsToNames(lines[i++]));

                // Print Conditionals
                string conditionalString = lines[i++];
                if(!string.IsNullOrEmpty(conditionalString)) {
                    string[] conditionals = conditionalString.Split(';');

                    if(conditionals.Length > 0) {
                        newOutput.Append(ConvertCommaSeperatedItemsToNames(conditionals[0]));
                    }
                    for(int j = 1; j < conditionals.Length; j++) {
                        newOutput.Append(";" + ConvertCommaSeperatedItemsToNames(conditionals[j]));
                    }
                }
                newOutput.AppendLine();

                // Print Time_Needed
                newOutput.AppendLine(lines[i++]);

                // Print Time_Available
                newOutput.AppendLine(lines[i++]);
            }

            return newOutput.ToString().Substring(0, newOutput.Length - 1);
        }

        private static string ConvertCommaSeperatedItemsToNames(string items) {
            StringBuilder output = new StringBuilder();

            if(!string.IsNullOrEmpty(items)) {
                string[] splitItems = items.Split(',');

                if(splitItems.Length > 0) {
                    output.Append(mmrMain.ItemDataToName(splitItems[0]));
                }
                for(int j = 1; j < splitItems.Length; j++) {
                    output.Append("," + mmrMain.ItemDataToName(splitItems[j]));
                }
            }

            return output.ToString();
        }
    }
}
