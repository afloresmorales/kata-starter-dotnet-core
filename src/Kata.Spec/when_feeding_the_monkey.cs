using FluentAssertions;
using FluentAssertions.Common;
using Machine.Specifications;
using System;

namespace Kata.Spec
{
    public class when_feeding_the_monkey
    {
        static Monkey _systemUnderTest;
        
        Establish context = () => 
            _systemUnderTest = new Monkey();

        Because of = () => 
            _systemUnderTest.Eat("banana");

        It should_have_the_food_in_its_belly = () =>
            _systemUnderTest.Belly.Should().Contain("banana");
    }

    public class when_airplane_requests_permission
    {
        Establish _context = () =>
        {
            _systemUnderTest = new Airport();
        };

        Because of = () => { _result = _systemUnderTest.Action(); };
        It should_return_landing_track_assingment = () => { _result.Should().Be(null); };
        static Airport _systemUnderTest;
    }
    interface IMediator
    {
        void Notify(object sender, string specificEvent);
    }

    class ConcreteMediator : IMediator
    {
        Airplane _airplane;
        LandingTrack _landingTrack;

        public ConcreteMediator(Airplane airplane, LandingTrack landingTrack)
        {
            this._airplane = airplane;
            this._airplane.SetMediator(this);
            this._landingTrack = landingTrack;
            this._landingTrack.SetMediator(this);
        }

        public void Notify(object sender, string specificEvent)
        {
            if (specificEvent == "Airplane")
            {
                Console.WriteLine("Mediator reacts on Airplane and triggers following Route");
                this._landingTrack.AssignLandingTrackForAirplanes();
            }
        }
    }

    class ControlTowerBase
    {
        protected IMediator _mediator;

        public ControlTowerBase(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }
    class Airport
    {
        static void Main(string[] arguments)
        {
            Airplane airplane = new Airplane();
            LandingTrack landingTrack = new LandingTrack();
            new ConcreteMediator(airplane, landingTrack);
            
            airplane.AirplaneRequestLandingPermission();
        }
    }

    internal class LandingTrack : ControlTowerBase
    {
        public string AssignLandingTrackForAirplanes()
        {
            return "Airplane should land in track #2.";
        }
    }


    internal class Airplane : ControlTowerBase
    {
        public void AirplaneRequestLandingPermission()
        {
            this._mediator.Notify(this,"Airplane");
        }
    }
}