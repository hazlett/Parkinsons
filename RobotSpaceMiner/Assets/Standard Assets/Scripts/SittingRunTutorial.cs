using OhioState.Libraries.Gesture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SittingRunTutorial : IGestureAction
{
    private SitStandTutor tutor;
    public SittingRunTutorial(SitStandTutor tutor)
    {
        this.tutor = tutor;
    }
    public void Trigger(object data)
    {
        
    }
}

