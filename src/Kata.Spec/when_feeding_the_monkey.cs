using FluentAssertions;
using FluentAssertions.Common;
using Machine.Specifications;
using System;

namespace Kata.Spec
{
    public class when_airplane_contacts_control_tower
    {
        Establish _context = () =>
        {
            _airplane = new Airplane();
            _landingTrackAssignation = new LandingTrackAssignation();
            new ConcreteMediator(_airplane, _landingTrackAssignation);
        };

        Because of = () => { _airplane.NotifyLandingApproximation(); };
        It should_trigger_following_operation = () => { _landingTrackAssignation.assignedTrack.Should().Be("Land in track #3"); };
        static Airplane _airplane;
        static LandingTrackAssignation _landingTrackAssignation;
    }


    internal interface IMediator
    {
        void Notify(object sender, string aircraft);
    }
    class ConcreteMediator : IMediator
    {
        Airplane _airplane;
        LandingTrackAssignation _landingTrack;
        public ConcreteMediator(Airplane airplane, LandingTrackAssignation landingTrackAssignation)
        {
            this._airplane = airplane;
            this._airplane.SetMediator(this);
            this._landingTrack = landingTrackAssignation;
            this._landingTrack.SetMediator(this);
        }

        public void Notify(object sender, string aircraft)
        {
            if (aircraft == "Boeing 777")
            {
                this._landingTrack.AssignLandingTrack();
            }
        }
    }

    internal class LandingTrackAssignation : BaseComponent
    {
        public string assignedTrack = "";
        public void AssignLandingTrack()
        {
            assignedTrack = "Land in track #3";
            
        }
    }

    internal class Airplane : BaseComponent
    {
        public void NotifyLandingApproximation()
        {
            this._mediator.Notify(this, "Boeing 777");
        }
    }

     class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }
    
}