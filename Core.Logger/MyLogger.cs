using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;

namespace Core.MyLogger
{
    public class MyLogger : Logger
    {
        public override void Initialize(IEventSource eventSource)
        {
            //Register for the ProjectStarted, TargetStarted, and ProjectFinished events
            eventSource.ProjectStarted += new ProjectStartedEventHandler(eventSource_ProjectStarted);
            eventSource.TargetStarted += new TargetStartedEventHandler(eventSource_TargetStarted);
            eventSource.ProjectFinished += new ProjectFinishedEventHandler(eventSource_ProjectFinished);
        }

        void eventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            Console.WriteLine("Project Started: " + e.ProjectFile);
        }

        void eventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            Console.WriteLine("Project Finished: " + e.ProjectFile);
        }

        void eventSource_TargetStarted(object sender, TargetStartedEventArgs e)
        {
            if (Verbosity == LoggerVerbosity.Detailed)
            {
                Console.WriteLine("Target Started: " + e.TargetName);
            }
        }
    }
}
