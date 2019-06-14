/*
   ____         _  ___ __   ___   ____ ____ 
  / __/_    __ (_)/ _// /_ / _ \ / __// __ \
 _\ \ | |/|/ // // _// __// ___/_\ \ / /_/ /
/___/ |__,__//_//_/  \__//_/   /___/ \____/    

 */
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

/// <summary>
/// Simplified from ProgressBar by Daniel S. Wolf, found here: https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54
/// </summary>
namespace SwiftPSO.Simulator
{
    /// <summary>
    /// An ASCII progress bar
    /// </summary>
    public class ProgressBar : IProgress<double>
    {
        private readonly int _blocks;

        private double _currentProgress;
        private string _currentText;

        public ProgressBar() : this(50)
        { }

        public ProgressBar(int blockCount)
        {
            _blocks = blockCount;
            _currentProgress = 0;
            _currentText = string.Empty;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Report(double value)
        {
            value = Math.Max(0, Math.Min(1, value));
            Interlocked.Exchange(ref _currentProgress, value);

            int progressBlockCount = (int)(_currentProgress * _blocks);
            int percent = (int)(_currentProgress * 100);
            string text = string.Format("[{0}{1}] {2,3}%", new string('#', progressBlockCount),
                new string('-', _blocks - progressBlockCount), percent);
            UpdateText(text);
        }

        private void UpdateText(string text)
        {
            // Get length of common portion
            int commonPrefixLength = 0;
            int commonLength = Math.Min(_currentText.Length, text.Length);
            while (commonPrefixLength < commonLength && text[commonPrefixLength] == _currentText[commonPrefixLength])
            {
                commonPrefixLength++;
            }

            // Backtrack to the first differing character
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.Append('\b', _currentText.Length - commonPrefixLength);

            // Output new suffix
            outputBuilder.Append(text.Substring(commonPrefixLength));

            // If the new text is shorter than the old one: delete overlapping characters
            int overlapCount = _currentText.Length - text.Length;
            if (overlapCount > 0)
            {
                outputBuilder.Append(' ', overlapCount);
                outputBuilder.Append('\b', overlapCount);
            }

            Console.Write(outputBuilder);
            _currentText = text;
        }
    }
}
