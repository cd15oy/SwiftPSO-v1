/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;
using System.Text.RegularExpressions;

namespace SwiftPSO.Core.Types
{
    public class Domain
    {
        public Bounds this[int index] => DimensionBounds[index];

        public Bounds[] DimensionBounds { get; private set; }

        public int Dimension => DimensionBounds.Length;

        /// <summary>
        /// Create a <see cref="Domain"/> with the specified <see cref="Bounds"/>
        /// </summary>
        /// <param name="bounds">An array of <see cref="Bounds"/> for each dimension.</param>
        public Domain(Bounds[] bounds)
        {
            DimensionBounds = bounds;
        }

        /// <summary>
        /// Create a <see cref="Domain"/> with the specified (fixed) range for each dimension.
        /// </summary>
        /// <param name="lower">The lower bound for each dimension.</param>
        /// <param name="upper">The upper bound for each dimension.</param>
        /// <param name="dimension">The number of dimensions</param>
        public Domain(double lower, double upper, int dimension)
        {
            DimensionBounds = new Bounds[dimension];
            for (int i = 0; i < dimension; i++)
            {
                DimensionBounds[i] = new Bounds(lower, upper);
            }
        }

        /// <summary>
        /// Create a <see cref="Domain"/> by parsing the domain string.
        /// </summary>
        /// <param name="d"></param>
        public Domain(string domainString)
        {
            Parse(domainString);
        }

        /// <summary>
        /// Create a clone of this <see cref="Domain"/>.
        /// </summary>
        /// <returns>A clone of this <see cref="Domain"/></returns>
        public Domain Clone()
        {
            Bounds[] clone = new Bounds[Dimension];
            for (int i = 0; i < clone.Length; i++)
            {
                clone[i] = DimensionBounds[i].Clone();
            }

            return new Domain(clone);
        }

        /// <summary>
        /// Create a <see cref="Domain"/> with the specified bounds
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        private void Fixed(double lower, double upper, int dimension)
        {
            DimensionBounds = new Bounds[dimension];
            for (int i = 0; i < dimension; i++)
            {
                DimensionBounds[i] = new Bounds(lower, upper);
            }
        }

        /// <summary>
        /// Create a <see cref="Domain"/> from its string representation.
        /// </summary>
        /// <param name="domainString">A string representing the <see cref="Domain"/> in the form: (lower, upper)^dimension</param>
        /// <returns>A parsed <see cref="Domain"/>.</returns>
        private void Parse(string domainString)
        {
            //TODO: expand this to have mixed domains (u1,l1)^d1...(un,ln)^dn
            Regex regex = new Regex(@"[(](?<lower>-*[0-9\.]+)\s*,\s*(?<upper>-*[0-9\.]+)[)]\^(?<dim>[0-9]+)");

            Match match = regex.Match(domainString);
            if (!match.Success) throw new ArgumentException($"Error in parsing domain: {domainString}");

            if (!double.TryParse(match.Groups["lower"].Value, out double lower))
            {
                throw new Exception($"The lower bound, {match.Groups["lower"].Value}, could not be parsed as a Double value. Fix the regex.");
            }

            if (!double.TryParse(match.Groups["upper"].Value, out double upper))
            {
                throw new Exception($"The upper bound, {match.Groups["upper"].Value}, could not be parsed as a Double value. Fix the regex.");
            }

            if (!int.TryParse(match.Groups["dim"].Value, out int dimension))
            {
                throw new Exception($"The dimension, {match.Groups["lower"].Value}, could not be parsed as an Integer value. Fix the regex.");
            }

            DimensionBounds = new Bounds[dimension];
            for (int i = 0; i < dimension; i++)
            {
                DimensionBounds[i] = new Bounds(lower, upper);
            }
        }
    }
}