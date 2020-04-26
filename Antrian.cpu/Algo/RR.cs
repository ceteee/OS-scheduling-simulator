using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Antrian.cpu.Process;

namespace Antrian.cpu.Algo
{
    public class RR
    {
        public List<Process> processes = new List<Process>();

        public List<Process> waitingProcesses = new List<Process>();

        public List<Process> bfp;

        private int completed;

        private int clockTime;

        private int elapsedClockTime;

        public RR(List<Process> bfp)
        {
            foreach (var p in bfp)
            {
                if (p.getPrioritas() == 2 && p.getClockAwal() == 0)
                    this.processes.Add(p);
            }
            foreach (var p in bfp)
            {
                if (p.getPrioritas() == 2 && p.getClockAwal() != 0)
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
            };

            Process process = null;
            int count = 0;
            List<int> indexedUncomplete = new List<int>();
            for (int i = 0; i < processes.Count; i++)
            {
                if (processes[i].getTest() != 0)
                {
                    count++;
                    indexedUncomplete.Add(i);
                }
            }

            if (count > 0)
            {
                int index = indexedUncomplete[((elapsedClockTime / 3) % count)];
                process = processes[index];

                elapsedClockTime++;
                int totalRound = 0;
                if (process.getTest() > 0)
                {
                    process.setTest(process.getBurstTime());
                    process.setBurstTime(process.getBurstTime() - 1);
                    totalRound = process.getRound() + 1;
                    process.setRound(totalRound);
                }
                if ((totalRound%4)==0)
                {
                    processes.RemoveAt(index);
                    return process;
                }
            }




            return new Process();



        }
    }
}