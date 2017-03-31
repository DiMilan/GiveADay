using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models.Exceptions;
using Castle.Core.Internal;

namespace GoedBezigWebApp.Models.GroupState
{
    public class EventsApprovedState:GroupState
    {
        public override void AddTask(ActivityTask task)
        {
            if (Group.TaskList == null)
            {
                throw new TaskListException("Nog geen draaiboek geïnitialiseerd");
            }
            if (task.Description.IsNullOrEmpty()) throw new TaskListException("de omschrijving van een taak is verplicht");
            if (task.FromDateTime != DateTime.MinValue & task.ToDateTime != DateTime.MinValue)
            {
                if (task.FromDateTime < DateTime.Now) throw new TaskListException("de begintijd moet in de toekomst liggen");
                if (task.ToDateTime < DateTime.Now) throw new TaskListException("de eindtijd moet in de toekomst liggen");
            }

            if (task.Activity == null) throw new TaskListException("geen event opgegeven");
            if (task.Activity.Accepted == false) throw new TaskListException("Enkel goedgekeurde evenementen komen in aanmerking");
            Group.TaskList.Add(task);
        }

        public EventsApprovedState(Group @group) : base(@group)
        {
        }
    }
}
