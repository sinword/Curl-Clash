// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC\u2019s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;

namespace Wave.Essence.Tracker.Model.Demo
{
	public class TrackerPose : MonoBehaviour
	{
		[SerializeField]
		private TrackerId m_TrackerType = TrackerId.Tracker0;
		public TrackerId TrackerType { get { return m_TrackerType; } set { m_TrackerType = value; } }

		[SerializeField]
		private GameObject m_Tracker = null;
		public GameObject Tracker { get { return m_Tracker; } set { m_Tracker = value; } }

		public Vector3 positionOffset = new Vector3(0, 0, 0);
		public Quaternion rotationOffset = Quaternion.Euler(0, 0, 0);
		public bool rotate90 = false;
		private void Update()
		{
			if (TrackerManager.Instance == null || m_Tracker == null) { return; }

			bool valid = TrackerManager.Instance.IsTrackerPoseValid(m_TrackerType);
			if (valid)
			{
				Vector3 trackerPos =  TrackerManager.Instance.GetTrackerPosition(m_TrackerType);
				transform.localPosition = positionOffset + (rotate90 ? new Vector3(-trackerPos.z, trackerPos.y, trackerPos.x) : trackerPos);// if rotate90, position = rotation of original position
				transform.localRotation = rotationOffset * TrackerManager.Instance.GetTrackerRotation(m_TrackerType);
			}

			//m_Tracker.SetActive(valid);
		}
	}
}
