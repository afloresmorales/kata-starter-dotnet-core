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

    public class when_control_tower_assigns_landing_track
    {
        Establish _context = () =>
        {
            _airplane = new Airplane();
            _landingTrack = new LandingTrackAssignation();
            new ConcreteMediator(_airplane, _landingTrack);
        };

        Because of = () => { _landingTrack.NotifyEta(); };
        It should_notify_client_eta_to_family_members = () => { _landingTrack.eta.Should().Be("eta: 30 minutes"); };
        It should_notify_passengers_to_keep_phones_off = () => { _airplane.phoneCommunication.Should().Be("Keep phones off."); };
        static Airplane _airplane;
        static LandingTrackAssignation _landingTrack;
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

        public void Notify(object sender, string evento)
        {
            if (evento == "Boeing 777")
            {
                this._landingTrack.AssignLandingTrack();
            }

            if (evento == "eta")
            {
                this._landingTrack.ConfirmEta();
                this._airplane.KeepPhonesOff();
            }
        }
    }

    internal class LandingTrackAssignation : BaseComponent
    {
        public string assignedTrack = "";
        public string eta = "";
        public void AssignLandingTrack()
        {
            assignedTrack = "Land in track #3";
            
        }

        public void NotifyEta()
        {
            this._mediator.Notify(this,"eta");
        }

        public void ConfirmEta()
        {
            eta = "eta: 30 minutes";
        }
    }

    internal class Airplane : BaseComponent
    {
        public string phoneCommunication = "";
        public void NotifyLandingApproximation()
        {
            this._mediator.Notify(this, "Boeing 777");
        }

        public void KeepPhonesOff()
        {
            phoneCommunication = "Keep phones off.";
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