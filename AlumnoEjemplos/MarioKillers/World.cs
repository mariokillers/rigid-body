﻿using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlumnoEjemplos.MarioKillers
{
    public class World
    {
        private List<RigidBody> bodies = new List<RigidBody>();
        /// <summary>
        /// The acceleration of gravity, in m/s^2.
        /// </summary>
        public Vector3 GravityAcceleration = new Vector3(0, -9.81f, 0);
        public bool GravityEnabled = false;

        /// <summary>
        /// Debug mode for the world. If enabled, draws bounding objects
        /// for rigid bodies.
        /// </summary>
        public bool DebugEnabled = false;

        /// <summary>
        /// Integrates the whole world forward in time.
        /// Uses Euler's method for integration.
        /// </summary>
        /// <param name="timeStep">Timestep in seconds</param>
        public void Step(float timeStep)
        {
            foreach (RigidBody body in this.bodies) {
                if (this.GravityEnabled)
                {
                    body.ApplyForce(body.Mass * this.GravityAcceleration);
                }
                body.LinearVelocity += body.Force * (1.0f / body.Mass) * timeStep;
                body.Position += body.LinearVelocity * timeStep;
                // Force has to be set to zero, otherwise it will be integrated next step
                body.Force = Vector3.Empty;
            }
        }

        public void AddBody(RigidBody body)
        {
            this.bodies.Add(body);
        }

        public void Render()
        {
            foreach (RigidBody body in this.bodies)
            {
                body.Render();
                if (this.DebugEnabled)
                {
                    body.BoundingSphere.render();
                }
            }
        }

        public void Dispose()
        {
            foreach (RigidBody body in this.bodies)
            {
                body.Dispose();
            }
        }
    }
}
