using System;
using System.Collections.Generic;
using System.Text;

namespace SmallSharpTools.ImageResizer
{
    public class ProgressEventArgs : EventArgs
    {
        private int _completed = 0;
        private int _total = 0;
        
        public ProgressEventArgs(int completed, int total)
        {
            _completed = completed;
            _total = total;
        }
        
        /// <summary>
        /// Completed item count
        /// </summary>
        public int Completed
        {
            get
            {
                return _completed;
            }
        }

        /// <summary>
        /// Total item count
        /// </summary>
        public int Total
        {
            get
            {
                return _total;
            }
        }
    }
}
