﻿using FluentAssertions;
using Kf.CANetCore31.Tools.RenamSolution.Presentation.WinFormsClient.Wizard;
using System.Collections.Generic;
using Xunit;

namespace Kf.CANetCore31.Tools.Tests.RenameSolution.Presentation.Wizard
{
    public sealed class WizardControllerTests
    {
        private static WizardController FiveStepWizardController
            => new WizardController(new List<WizardStep>
            {
                WizardStep.Create("Step 1"),
                WizardStep.Create("Step 2"),
                WizardStep.Create("Step 3"),
                WizardStep.Create("Step 4"),
                WizardStep.Create("Step 5"),
            });

        [Fact]
        public void When_no_steps_given_controller_builds()
        {
            var sut = new WizardController(null);
            sut.Current.Should().Be(WizardStep.Empty);
            sut.AmountOfSteps.Should().Be(0);
            sut.CurrentStepNumber.Should().Be(0);
            sut.HasNextStep.Should().BeFalse();
            sut.Next().Should().Be(sut.Current);
        }

        [Fact]
        public void When_steps_given_controller_builds()
        {
            var sut = FiveStepWizardController;
            sut.Current.Should().Be(WizardStep.Create("Step 1"));
            sut.AmountOfSteps.Should().Be(5);
            sut.CurrentStepNumber.Should().Be(1);
        }

        [Fact]
        public void Step_next_navigation_cycles_and_returns_last_step_continuously()
        {
            var sut = FiveStepWizardController;
            sut.HasNextStep.Should().BeTrue();
            sut.CurrentStepNumber.Should().Be(1);
            sut.Next().Should().Be(WizardStep.Create("Step 2"));
            sut.CurrentStepNumber.Should().Be(2);
            sut.Next().Should().Be(WizardStep.Create("Step 3"));
            sut.CurrentStepNumber.Should().Be(3);
            sut.Next().Should().Be(WizardStep.Create("Step 4"));
            sut.CurrentStepNumber.Should().Be(4);
            sut.Next().Should().Be(WizardStep.Create("Step 5"));
            sut.CurrentStepNumber.Should().Be(5);
            sut.Next().Should().Be(WizardStep.Create("Step 5"));
            sut.CurrentStepNumber.Should().Be(5);
            sut.HasNextStep.Should().BeFalse();
        }
    }
}
