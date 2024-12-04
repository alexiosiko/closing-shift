using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
	[SerializeField] bool active = false;
	public void SetActive()
	{
		active = true;
		GameObject[] objs = GameObject.FindGameObjectsWithTag("Task");
		foreach (GameObject obj in objs)
			obj.GetComponent<Collider>().enabled = true;
	}
	public void Update()
	{
		if (!active)
			return;
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (tasksParent.gameObject.activeInHierarchy)
				tasksParent.gameObject.SetActive(false);
			else
				tasksParent.gameObject.SetActive(true);	
		}
	}
	[SerializeField] Transform tasksParent;
	int tasksLeft;
	public void CompleteTask(int index)
	{
		TMP_Text taskText = tasksParent.GetChild(index).GetComponent<TMP_Text>();
        taskText.text = $"<s>{taskText.text}</s>";
		tasksLeft--;
		if (IsTasksComplete())
			Complete();
	}
	void Complete()
	{
		AlertManager.Singleton.Alert("Okay that's all the tasks done! I can finally leave!");
	}
	void Awake()
	{
		tasksLeft = tasksParent.childCount;
		Singleton = this;
	}
	public bool IsTasksComplete() => tasksLeft == 0;
	public static TaskManager Singleton;
}
