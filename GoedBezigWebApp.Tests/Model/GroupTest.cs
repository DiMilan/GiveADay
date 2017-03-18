using System;
using System.Collections.Generic;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.Exceptions;
using GoedBezigWebApp.Models.MotivationState;
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

        [Fact]
        private void NewGroupWithNameIsCreatedCorrectly()
        {
            Group group = new Group("testGroup", false);
            Assert.Equal("testGroup", group.GroupName);
            Assert.Equal(false, group.ClosedGroup);
            Assert.InRange(group.Timestamp, DateTime.Now.Subtract(TimeSpan.FromSeconds(1)), DateTime.Now);
            Assert.True(group.MotivationStatus is OpenState);
            Assert.True(group.Invitations is List<Invitation>);
            Assert.True(group.Events is List<Event>);
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
        [Fact]
        private void SavingMotivationInOpenGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus.SaveMotivation(GeldigeMotivation);
            Assert.Equal(group.Motivation, GeldigeMotivation);
        }

        [Fact]
        private void SavingMotivationInDeclinedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new DeclinedState(group);
            group.MotivationStatus.SaveMotivation(GeldigeMotivation);
            Assert.Equal(group.Motivation, GeldigeMotivation);
            Assert.True(group.MotivationStatus is OpenState);
        }
        [Fact]
        private void SavingMotivationInSubmittedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new SubmittedState(group);
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SaveMotivation(GeldigeMotivation));

        }
        [Fact]
        private void SavingMotivationInApprovedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new ApprovedState(group);
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SaveMotivation(GeldigeMotivation));

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
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SubmitMotivation());
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
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SubmitMotivation());
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
            group.MotivationStatus.SubmitMotivation();
            Assert.True(group.MotivationStatus is SubmittedState);
        }

        [Fact]
        private void SubmittingMotivationInDeclinedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new DeclinedState(group);
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SubmitMotivation());
        }
        [Fact]
        private void SubmittingMotivationInSubmittedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new SubmittedState(group);
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SubmitMotivation());

        }
        [Fact]
        private void SubmittingMotivationInApprovedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new ApprovedState(group);
            Assert.Throws<MotivationException>(() => group.MotivationStatus.SubmitMotivation());

        }

        [Fact]
        private void SavingCompanyDetailsInOpenGroup()
        {
            Group group = new Group("testGroup", false);

            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            group.MotivationStatus.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite);
            Assert.Equal(group.CompanyName, companyName);
            Assert.Equal(group.CompanyAddress, companyAddress);
            Assert.Equal(group.CompanyEmail, companyEmail);
            Assert.Equal(group.CompanyWebsite, companyWebsite);
        }

        [Fact]
        private void SavingCompanyDetailsInDeclinedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new DeclinedState(group);
            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            group.MotivationStatus.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite);
            Assert.Equal(group.CompanyName, companyName);
            Assert.Equal(group.CompanyAddress, companyAddress);
            Assert.Equal(group.CompanyEmail, companyEmail);
            Assert.Equal(group.CompanyWebsite, companyWebsite);
        }
        [Fact]
        private void SavingCompanyDetailsInSubmittedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new SubmittedState(group);
            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            Assert.Throws<MotivationException>(() => group.MotivationStatus.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite));

        }
        [Fact]
        private void SavingCompanyDetailsInApprovedGroup()
        {
            Group group = new Group("testGroup", false);
            group.MotivationStatus = new ApprovedState(group);
            string companyName = "testBedrijf";
            string companyAddress = "teststraat 23, 9000 Gent";
            string companyEmail = "test@test.be";
            string companyWebsite = "www.test.be";
            Assert.Throws<MotivationException>(() => group.MotivationStatus.AddCompanyDetails(companyName, companyAddress, companyEmail, companyWebsite));

        }


    }
}
