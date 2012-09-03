﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pulsar4X.Stargen;
using Pulsar4X.Entities;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace Pulsar4X.ViewModels
{
    public class StarSystemViewModel : INotifyPropertyChanged
    {
        private StarSystem _currentstarsystem;
        public StarSystem CurrentStarSystem
        {
            get { return _currentstarsystem; }
            set
            {
                _currentstarsystem = value;
                //NotifyPropertyChanged("CurrentStarSystem");
                OnPropertyChanged(() => CurrentStarSystem);
                CurrentStarSystemAge = _currentstarsystem.Stars[0].Age.ToString();
                Stars = new BindingList<Star>(_currentstarsystem.Stars);
                StarsSource.DataSource = Stars;
            }
        }
        public BindingList<StarSystem> StarSystems { get; set; }

        private string _currentstarsystemage;
        public string CurrentStarSystemAge
        {
            get { return _currentstarsystemage; }
            set
            {
                _currentstarsystemage = value;
                //NotifyPropertyChanged("CurrentStarSystemAge");
                OnPropertyChanged(() => CurrentStarSystemAge);
            }
        }

        private BindingList<Star> _stars;
        public BindingList<Star> Stars
        {
            get { return _stars; }
            set
            {
                _stars = value;
                //NotifyPropertyChanged("Stars");
                OnPropertyChanged(() => Stars);
                CurrentStar = _stars[0];
            }
        }

        private BindingSource _starssource;
        public BindingSource StarsSource
        {
            get
            {
                if (_starssource == null)
                    _starssource = new BindingSource();
                return _starssource;
            }
            set
            {
                _starssource = value;
            }
        }

        private BindingSource _planetsource;
        public BindingSource PlanetSource
        {
            get
            {
                if (_planetsource == null)
                    _planetsource = new BindingSource();
                return _planetsource;
            }
            set
            {
                _planetsource = value;
            }
        }

        private Star _currentstar;
        public Star CurrentStar
        {
            get
            {
                return _currentstar;
            }
            set
            {
                _currentstar = value;
                //NotifyPropertyChanged("CurrentStar");
                OnPropertyChanged(() => CurrentStar);
                PlanetSource.DataSource = _currentstar.Planets;
            }
        }
        public Planet CurrentPlanet { get; set; }

        public bool isSM { get; set; }
        public bool isNotSM { get { return !isSM; } set { isSM = !value; } }

        public StarSystemViewModel()
        {
            // Just gen a Starsystem
            var ssf = new StarSystemFactory(false);
            var ss = ssf.Create("Test");
            var ss2 = ssf.Create("Foo");
            var ss3 = ssf.Create("Bar");
            StarSystems = new BindingList<StarSystem>();
            StarSystems.Add(ss);
            StarSystems.Add(ss2);
            StarSystems.Add(ss3);
            CurrentStarSystem = ss;
            CurrentStar = CurrentStarSystem.Stars.First();
            CurrentPlanet = CurrentStar.Planets.First();
        }

        private void OnPropertyChanged(Expression<Func<object>> property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(BindingHelper.Name(property)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}