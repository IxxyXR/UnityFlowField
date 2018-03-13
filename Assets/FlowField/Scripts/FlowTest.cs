using System;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FlowTest : MonoBehaviour {

	public bool AutoAdjustColor = false;
	public bool UseTriggerVolume = false;
	public bool UseTrails = true;
	public Texture2D FlowMap;
	public Vector2 offset = new Vector2(1,1);
	public Vector2 scale = new Vector2(50,50);
	public int preset = 0;
	
	private ParticleSystem system;
	private ParticleSystem.Particle[] particles;
	private float maxVelocity = 0f;
	private float TOLERANCE = 0.1f;

	void Start() {
		if (system == null) {
			system = GetComponent<ParticleSystem>();
		}
	}

	private void LateUpdate () {
		if (particles == null || particles.Length < system.maxParticles) {
			particles = new ParticleSystem.Particle[system.maxParticles];
		}
		
		var trigger = system.trigger;
		trigger.enabled = UseTriggerVolume;
		var trails = system.trails;
		trails.enabled = UseTrails;
		
		int particleCount = system.GetParticles(particles);
		PositionParticles();
		system.SetParticles(particles, particleCount);
	}
	
	private void PositionParticles () {
		
		for (int i = 0; i < particles.Length; i++) {
			
			var p = particles[i].position;

			if (FlowMap != null) {
				var pix = FlowMap.GetPixel((int) ((p.x + offset.x) * scale.x), (int) ((p.y + offset.y) * scale.y));
				particles[i].velocity = new Vector3(
					-0.5f + pix.r,
					-0.5f + pix.g,
					-0.5f + pix.b
				);
			}

			switch (preset) {
				case 0:
					break;
				case 1:
					particles[i].velocity = new Vector3(
						p.y/Mathf.Cos(p.magnitude),
						Mathf.Max((Mathf.Log(p.y)+p.x),p.x),
						p.z/Mathf.Sin(p.magnitude)
					);
					break;
				case 2:
					particles[i].velocity = new Vector3(
						p.y,
						Mathf.Cos(
							Mathf.Min(
								p.x/p.magnitude*Mathf.Cos(p.y),
								p.y
							)
						),
						0.2f
					);
					break;
			}
			
			if (AutoAdjustColor) {
				maxVelocity = Mathf.Max(
					maxVelocity,
					particles[i].velocity.x,
					particles[i].velocity.y,
					particles[i].velocity.z
				);
			}
			
			
		}

		if (AutoAdjustColor) {
			if (Math.Abs(system.colorBySpeed.range[1] - maxVelocity) > TOLERANCE) {
				var col = system.colorBySpeed;
				col.range = new Vector2(0, maxVelocity);
			}
		}
	}
	
}