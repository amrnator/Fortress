using System.Collections;
using UnityEngine;

public class JobManager : MonoBehaviour {

    // organize jobs and assign tasks to dwarfs

    // a job is a set of interactables
    // dwarfs need to complete all interactions for the job to be done
    // player can set occupations for a dwarf, this occupation specilizes in a certain type of job (woodcutters cut trees)
    // player can then assign how many dwarfs will perform a job, 
    // this number cannot exceed the amount of dwarfs in the specific occupation

    public static JobManager instance = null;

    public ArrayList jobs;

    public ArrayList dwarves;

    [Header("Test objects")]
    public Job test;
    public Interactable tree;
    public Interactable stone;

    private void Awake()
    {
        //Sigleton
        if (instance == null)
            instance = this;
        
            else if (instance != this)
                Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //get dwarves in the scene
        dwarves = new ArrayList();

        GameObject[] d = GameObject.FindGameObjectsWithTag("Dwarf");

        foreach (GameObject x in d)
        {
            dwarves.Add(x.GetComponent<DwarfControl>());
        }

        jobs = new ArrayList();

        //test jobss
        test = new Job(Job.JobType.Build, new Interactable[] { tree }, 1);
        jobs.Add(test);

        test = new Job(Job.JobType.Harvest, new Interactable[] { stone }, 2);
        jobs.Add(test);

    }

    public void AddJob(Job newJob){
        jobs.Add(newJob);
    }

    //TODO might want to make a slower update
    private void Update()
    {
        foreach (Job j in jobs) 
        {
            if(!j.isComplete)
            {
                j.cycle();    
            }
        }
    }

}

public class Job : ScriptableObject
{
    public enum JobType { Harvest, Build };

    public JobType jobType;

    public Interactable[] interactables; // list of objects that need to be interacted

    public ArrayList assignedDwarves; // list of dwarves who are currently working this job

    public bool isComplete = false; // flag for when job is done

    int workerNum; //number of allocated workers

    public Job(JobType type, Interactable[] inter, int size){
        jobType = type;
        interactables = inter;
        workerNum = size;
    }

    private void Awake()
    {
        assignedDwarves = new ArrayList();
    }

    public void cycle()
    {
        // find free workers that are assigned to this kind of job
        foreach(DwarfControl dwarf in JobManager.instance.dwarves)
        {
            if((dwarf.skills.occupation == this.jobType) && !dwarf.isWorking && !dwarf.isTasked && assignedDwarves.Count < workerNum){
                //add dwarf to job
                assignedDwarves.Add(dwarf);
                dwarf.isWorking = true;
            }
        }

        // assign tasks to workers
        foreach(DwarfControl dwarf in assignedDwarves)
        {
            if (!dwarf.isTasked)
            {
                for (int i = 0; i < interactables.Length; i++)
                {
                    dwarf.setTask("Interact", interactables[i]);
                    dwarf.isTasked = true;
                }
            }
        }

        // check if tasks have been completed
        bool track = false;

        foreach(Interactable interact in interactables)
        {
            if(!interact.harvested)
            {
                track = true;
            }
        }

        if(!track){
            // job is done
            isComplete = true;

            foreach (DwarfControl dwarf in assignedDwarves) // free dwarves
            {
                dwarf.isWorking = false;
            }
        }
    }
}
