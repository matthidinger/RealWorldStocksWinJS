(function() {
    WinJS.Namespace.define("RealWorldStocks.Utils", {

        extend: function () {
            var options, name, src, copy,
		        target = arguments[0] || {},
		        i = 1,
		        length = arguments.length;

            for (; i < length; i++) {
                // Only deal with non-null/undefined values
                if ((options = arguments[i]) != null) {
                    // Extend the base object
                    for (name in options) {
                        src = target[name];
                        copy = options[name];

                        // Prevent never-ending loop
                        if (target === copy) {
                            continue;
                        }

                        if (copy !== undefined) {
                            target[name] = copy;
                        }
                    }
                }
            }

            // Return the modified object
            return target;
        },


        csvToArray: function (data, delimiter) {
            ///<summary>
            /// This will parse a delimited string into an array of
            /// arrays. The default delimiter is the comma, but this
            /// can be overriden in the second argument.
            ///</summary>
            /// <param name="data" type="String">
            /// The string of data to parse
            /// </param>
            /// <param name="delimiter" type="String" optional="true">
            /// The delimiter
            /// </param>
            /// <returns type="Array"></returns>

            delimiter = (delimiter || ",");

            // Create a regular expression to parse the CSV values.
            var objPattern = new RegExp(
			(
				// Delimiters.
				"(\\" + delimiter + "|\\r?\\n|\\r|^)" +

				// Quoted fields.
				"(?:\"([^\"]*(?:\"\"[^\"]*)*)\"|" +

				// Standard fields.
				"([^\"\\" + delimiter + "\\r\\n]*))"
			),
			"gi"
			);


            // Create an array to hold our data. Give the array
            // a default empty first row.
            var arrData = [[]];

            // Create an array to hold our individual pattern
            // matching groups.
            var arrMatches = null;


            // Keep looping over the regular expression matches
            // until we can no longer find a match.
            while (arrMatches = objPattern.exec(data)) {

                // Get the delimiter that was found.
                var strMatchedDelimiter = arrMatches[1];

                // Check to see if the given delimiter has a length
                // (is not the start of string) and if it matches
                // field delimiter. If id does not, then we know
                // that this delimiter is a row delimiter.
                if (
				strMatchedDelimiter.length &&
				(strMatchedDelimiter != delimiter)
				) {

                    // Since we have reached a new row of data,
                    // add an empty row to our data array.
				    arrData.push([]);

				}


                // Now that we have our delimiter out of the way,
                // let's check to see which kind of value we
                // captured (quoted or unquoted).
                if (arrMatches[2]) {

                    // We found a quoted value. When we capture
                    // this value, unescape any double quotes.
                    var strMatchedValue = arrMatches[2].replace(
					new RegExp("\"\"", "g"),
					"\""
					);

                } else {

                    // We found a non-quoted value.
                    var strMatchedValue = arrMatches[3];

                }


                // Now that we have our value string, let's add
                // it to the data array.
                arrData[arrData.length - 1].push(strMatchedValue);
            }

            // Return the parsed data.
            return (arrData);
        }
    });

})();