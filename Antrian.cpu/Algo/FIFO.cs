using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Antrian.cpu.Process;

namespace Antrian.cpu.Algo
{
    public class FIFO
    {
        public List<Process> processes = new List<Process>();

        public List<Process> waitingProcesses = new List<Process>();

        public List<Process> bfp;

        private int completed;

        private int clockTime;


        private int elapsedClockTime;


        public FIFO(List<Process> bfp)
        {
            foreach (var p in bfp)
            {
                if (p.getPrioritas() == 1 && p.getClockAwal() == 0)
                    this.processes.Add(p);
            }
            foreach (var p in bfp)
            {
                if (p.getPrioritas() == 1 && p.getClockAwal() != 0)
                    this.waitingProcesses.Add(p);
            }

            this.clockTime = 0;
            this.completed = 0;
        }


        public Process tick()
        {
            clockTime++;
            completed = 0;
            foreach (var p in waitingProcesses)
            {
                if (p.getClockAwal().Equals(clockTime))
                {
                    processes.Add(p);
                }

            }
            

            Process process = null;
            for (int i = 0; i < processes.Count; i++)
            {


                int totalElapsedTime = 0;
                if (processes[i].getBurstTime() > 0)
                {
                    process = processes[i];

                    process.setBurstTime(process.getBurstTime() - 1);
                    totalElapsedTime = process.getTime() + 1;
                    process.setElapsedClock(totalElapsedTime);
                    if (totalElapsedTime == 11)
                    {
                        processes.RemoveAt(i);
                        return process;
                    }
                    break;

                }


            }








            return new Process();



        }

    }
}