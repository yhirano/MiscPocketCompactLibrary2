using System;

using OpenNETCF.Threading;

namespace MiscPocketCompactLibrary2.Threading
{
    /// <summary>
    /// Read-write lock class
    /// </summary>
    public class ReaderWriterLock
    {
        Monitor2 monitor = new Monitor2();

        private int readingReaders = 0;

        private int waitingWriters = 0;

        private bool preferWriter = true;

        private bool writing = false;

        /// <summary>
        /// Acquire reader lock.
        /// </summary>
        public void AcquireReaderLock()
        {
            lock (this)
            {
                while (writing == true || (preferWriter == true && waitingWriters > 0))
                {
                    monitor.Wait();
                }
                ++readingReaders;
            }
        }

        /// <summary>
        /// Release reader lock.
        /// </summary>
        public void ReleaseReaderLock()
        {
            lock (this)
            {
                --readingReaders;
                preferWriter = true;
                monitor.PulseAll();
            }

        }

        /// <summary>
        /// Acquire writer lock.
        /// </summary>
        public void AcquireWriterLock()
        {
            lock (this)
            {
                ++waitingWriters;
                while (writing == true || readingReaders > 0)
                {
                    monitor.Wait();
                }
                --waitingWriters;
                writing = true;
            }
        }

        /// <summary>
        /// Release writer lock.
        /// </summary>
        public void ReleaseWriterLock()
        {
            lock (this)
            {
                writing = false;
                preferWriter = false;
                monitor.PulseAll();
            }
        }
    }
}
