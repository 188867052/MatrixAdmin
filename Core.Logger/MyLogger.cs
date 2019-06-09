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
            eventSource.ProjectStarted += new ProjectStartedEventHandler(EventSource_ProjectStarted);
            eventSource.TargetStarted += new TargetStartedEventHandler(EventSource_TargetStarted);
            eventSource.ProjectFinished += new ProjectFinishedEventHandler(EventSource_ProjectFinished);
        }

        void EventSource_ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            Console.WriteLine("Project Started: " + e.ProjectFile);
        }

        void EventSource_ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            Console.WriteLine("Project Finished: " + e.ProjectFile);
        }

        void EventSource_TargetStarted(object sender, TargetStartedEventArgs e)
        {
            if (Verbosity == LoggerVerbosity.Detailed)
            {
                Console.WriteLine("Target Started: " + e.TargetName);
            }
        }
    }
}
