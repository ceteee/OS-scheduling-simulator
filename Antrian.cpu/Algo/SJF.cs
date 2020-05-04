using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Antrian.cpu.Process;

namespace Antrian.cpu.Algo
{
    public class SJF
    {
        public List<Process> processes = new List<Process>();

        public List<Process> waitingProcesses = new List<Process>();

        public List<Process> bfp;

        private int completed;

        private int clockTime;



        public SJF(List<Process> bfp)
        {
            foreach (var p in bfp)
            {
                if (p.getPrioritas() == 3 && p.getClockAwal() == 0)
                    this.processes.Add(p);
            }
            this.processes = processes.OrderBy(process => process.getBurstTime()).ToList();
            foreach (var p in bfp)
            {
                if (p.getPrioritas() == 3 && p.getClockAwal() != 0)
                    this.waitingProcesses.Add(p);
            }

            this.clockTime = 0;
            this.completed = 0;
        }


        public List<Process> tick()
        {
            clockTime++;
            completed = 0;
            foreach (var p in waitingProcesses)
            {
                if (p.getClockAwal().Equals(clockTime))
                {
                    processes.Add(p);
                }
            };

            Process process = null;
            int selected = 0;

            foreach (var p in processes)
            {
                if (p.getBurstTime() == 0) completed++;
            };
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[i].getBurstTime() != 0)
                {
                    process = processes[i];
                    selected = i;
                    break;
                }
            }


            List<Process> demo = new List<Process>();

            if (process != null)
            {
                for (int i = selected + 1; i < processes.Count; i++)
                {
                    if (processes[i].getBurstTime() != 0)
                    {
                        processes[i].setWaitingClock(processes[i].getWaitingClock() + 1);
                        if (processes[i].getWaitingClock() >= 25)
                        {

                            demo.Add(processes[i]);
                            processes.RemoveAt(i);
                            i--;

                        }

                    }
                }
            }


            if (process != null && process.getClockAwal() == 0)
            {
                process.setFirst(!process.getFirst());
                process.setBurstTime(process.getBurstTime() - 1);
            }

            if (process != null && process.getFirst() == false)
            {

                process.setBurstTime(process.getBurstTime() - 1);
            }
            if (process != null && process.getFirst() == true)
            {
                process.setFirst(!process.getFirst());
                process.setBurstTime(process.getBurstTime());
            }


            if (demo.Count > 0)
            {
                return demo;
            }
            return new List<Process>();
        }

    }
}