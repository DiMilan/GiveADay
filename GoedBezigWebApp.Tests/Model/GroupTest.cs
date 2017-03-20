using System;
using System.Collections.Generic;
using System.Linq;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.GroupState;
using Xunit;

namespace GoedBezigWebApp.Tests.Model
{
    public class GroupTest
    {
        private Group _testGroup;
        private string GeldigeMotivation { get; set; }
        public GroupTest()
        {
            _testGroup = new Group("testGroup", false);
            GeldigeMotivation = @"Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! ";
        }
        #region Constructors

        [Fact]
        private void NewGroupWithNameIsCreatedCorrectly()
        {
            Group group = new Group("testGroup", false);
            Assert.Equal("testGroup", group.GroupName);
            Assert.Equal(false, group.ClosedGroup);
            Assert.InRange(group.Timestamp, DateTime.Now.Subtract(TimeSpan.FromSeconds(1)), DateTime.Now);
            Assert.True(group.GroupState is MotivationOpenState);
            Assert.True(group.Invitations is List<Invitation>);
            Assert.True(group.Activities is List<Activity>);
            Assert.Null(group.Motivation);
            Assert.Null(group.CompanyName);
            Assert.Null(group.CompanyAddress);
            Assert.Null(group.CompanyEmail);
            Assert.Null(group.CompanyWebsite);
            Assert.Null(group.CompanyContactName);
            Assert.Null(group.CompanyContactSurname);
            Assert.Null(group.CompanyContactTitle);
            Assert.Null(group.CompanyContactEmail);

        }
        #endregion

        #region Moivation
        [Fact]
        private void SavingMotivationInOpenGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState.SaveMotivation(GeldigeMotivation);
            Assert.Equal(group.Motivation, GeldigeMotivation);
        }

        [Fact]
        private void SavingMotivationInDeclinedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationDeclinedState(group);
            group.GroupState.SaveMotivation(GeldigeMotivation);
            Assert.Equal(group.Motivation, GeldigeMotivation);
            Assert.True(group.GroupState is MotivationOpenState);
        }
        [Fact]
        private void SavingMotivationInSubmittedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationSubmittedState(group);
            Assert.Throws<MotivationException>(() => group.GroupState.SaveMotivation(GeldigeMotivation));

        }
        [Fact]
        private void SavingMotivationInApprovedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationApprovedState(group);
            Assert.Throws<MotivationException>(() => group.GroupState.SaveMotivation(GeldigeMotivation));

        }
        [Fact]
        private void SubmittingTooShortMotivation()
        {
            Group group = new Group("testGroup", false);
            string ongeldigeMotivation = @"Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed.";
            group.Motivation = ongeldigeMotivation;
            group.CompanyName = "testBedrijf";
            group.CompanyAddress = "teststraat 23, 9000 Gent";
            group.CompanyEmail = "test@test.be";
            group.CompanyWebsite = "www.test.be";
            Assert.Throws<MotivationException>(() => group.GroupState.SubmitMotivation());
        }

        [Fact]
        private void SubmittingTooLongMotivation()
        {
            Group group = new Group("testGroup", false);
            string ongeldigeMotivation = @"Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! Wij schenken het label van ""goed bezig"" aan Sanctuary De Zonnegloed. Dit is een toevluchtsoord voor verwaarloosde en in beslag genomen wilde dieren. De vrijwilligers zetten zich elke dag belangeloos in! Ze zijn Goed bezig! ";
            group.Motivation = ongeldigeMotivation;
            group.CompanyName = "testBedrijf";
            group.CompanyAddress = "teststraat 23, 9000 Gent";
            group.CompanyEmail = "test@test.be";
            group.CompanyWebsite = "www.test.be";
            Assert.Throws<MotivationException>(() => group.GroupState.SubmitMotivation());
        }

        [Fact]
        private void SubmittingMotivationInOpenGroup()
        {
            Group group = new Group("testGroup", false);
            group.Motivation = GeldigeMotivation;
            group.CompanyName = "testBedrijf";
            group.CompanyAddress = "teststraat 23, 9000 Gent";
            group.CompanyEmail = "test@test.be";
            group.CompanyWebsite = "www.test.be";
            group.GroupState.SubmitMotivation();
            Assert.True(group.GroupState is MotivationSubmittedState);
        }

        [Fact]
        private void SubmittingMotivationInDeclinedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationDeclinedState(group);
            Assert.Throws<MotivationException>(() => group.GroupState.SubmitMotivation());
        }
        [Fact]
        private void SubmittingMotivationInSubmittedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationSubmittedState(group);
            Assert.Throws<MotivationException>(() => group.GroupState.SubmitMotivation());

        }
        [Fact]
        private void SubmittingMotivationInApprovedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationApprovedState(group);
            Assert.Throws<MotivationException>(() => group.GroupState.SubmitMotivation());

        }

        [Fact]
        private void SavingCompanyDetailsInOpenGroup()
        {
            Group group = new Group("testGroup", false);

            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            group.GroupState.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite);
            Assert.Equal(group.CompanyName, companyName);
            Assert.Equal(group.CompanyAddress, companyAddress);
            Assert.Equal(group.CompanyEmail, companyEmail);
            Assert.Equal(group.CompanyWebsite, companyWebsite);
        }

        [Fact]
        private void SavingCompanyDetailsInDeclinedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationDeclinedState(group);
            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            group.GroupState.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite);
            Assert.Equal(group.CompanyName, companyName);
            Assert.Equal(group.CompanyAddress, companyAddress);
            Assert.Equal(group.CompanyEmail, companyEmail);
            Assert.Equal(group.CompanyWebsite, companyWebsite);
        }
        [Fact]
        private void SavingCompanyDetailsInSubmittedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationSubmittedState(group);
            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            Assert.Throws<MotivationException>(() => group.GroupState.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite));

        }
        [Fact]
        private void SavingCompanyDetailsInApprovedGroup()
        {
            Group group = new Group("testGroup", false);
            group.GroupState = new MotivationApprovedState(group);
            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            Assert.Throws<MotivationException>(() => group.GroupState.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite));

        }
        #endregion

        #region Tasks

        [Fact]
        private void AddingTaskWithoutTasklist()
        {
            ActivityTask task = new ActivityTask(null, null, null, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }

        [Fact]
        private void AddingTaskSucceeds()
        {
            Event activityEvent = new Event("test", "testOmschrijving", DateTime.Today) { Accepted = true };
            _testGroup.InitiateTaskList();
            ActivityTask task = new ActivityTask("description", null, activityEvent, TaskState.Done);
            _testGroup.AddTask(task);
            Assert.Equal(_testGroup.TaskList.FirstOrDefault().Activity, activityEvent);
            Assert.Equal(_testGroup.TaskList.FirstOrDefault().Description, "description");
            Assert.Equal(_testGroup.TaskList.FirstOrDefault().CurrentState, TaskState.Done);
            Assert.Equal(_testGroup.TaskList.FirstOrDefault().Users, null);
        }
        [Fact]
        private void AddingTaskWithoutDescription()
        {
            Event activityEvent = new Event("test", "testOmschrijving", DateTime.Today) { Accepted = true };
            _testGroup.InitiateTaskList();
            ActivityTask task = new ActivityTask(null, null, activityEvent, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }
        [Fact]
        private void AddingTaskWithFromDateInPast()
        {
            Event activityEvent = new Event("test", "testOmschrijving", DateTime.Today) { Accepted = true };
            _testGroup.InitiateTaskList();
            ActivityTask task = new ActivityTask("description", null, new DateTime(2016, 1,1), DateTime.Now, activityEvent, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }
        [Fact]
        private void AddingTaskWithToDateInPast()
        {
            Event activityEvent = new Event("test", "testOmschrijving", DateTime.Today) { Accepted = true };
            ActivityTask task = new ActivityTask("description", null, DateTime.MaxValue, new DateTime(2016, 1, 1), activityEvent, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }
        [Fact]
        private void AddingTaskWithoutApprovedEvent()
        {
            _testGroup.InitiateTaskList();
            Event activityEvent = new Event("test", "testOmschrijving", DateTime.Today) {Accepted = false};
            ActivityTask task = new ActivityTask("description", null, DateTime.MaxValue, new DateTime(2016, 1, 1), activityEvent, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }
        [Fact]
        private void AddingTaskWithoutDates()
        {
            _testGroup.InitiateTaskList();
            ActivityTask task = new ActivityTask("description", null, null, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }
        [Fact]
        private void AddingTaskWithoutUsers()
        {
            _testGroup.InitiateTaskList();
            ActivityTask task = new ActivityTask("description", null, DateTime.MaxValue, DateTime.MaxValue, null, TaskState.Done);
            Assert.Throws<TaskListException>(() => _testGroup.AddTask(task));
        }

        #endregion
    }
}
