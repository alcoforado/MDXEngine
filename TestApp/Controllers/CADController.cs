﻿using MDXEngine;
using MDXEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Controllers
{
    public class CADController: IController
    {
        IDxViewControl _dx;
        IAppStateProvider _appStateProvider;
        public CADController(IDxViewControl dx, IAppStateProvider appState)
        {
            _dx = dx;
            _appStateProvider = appState;
        }

     


        public void SetLights(DirectionalLight model)
        {
           // _appStateProvider.GetAppState<>
            return; 

        }
        


        }
    }


