using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antrian.cpu
{
    public class Process
    {
        public Process(){

        }
        public Process(int id, int prioritas, int lamaProses, int burstTime, int aksesIO, int clockAwal)
        {
            this.id = id;
            this.prioritas = prioritas;
            this.lamaProses = lamaProses;
            this.burstTime = burstTime;
            this.aksesIO = aksesIO;
            this.clockAwal = clockAwal;
            this.waitingClock = 0;
            this.round = 0;
        }

        private int id;

        private int prioritas;

        private int lamaProses;

        private int burstTime;

        private int aksesIO;

        private int clockAwal;

        private int waitingClock;

        private int round;


        public int getRound()
        {
            return round;
        }

        public void setRound(int round)
        {
            this.round = round;
        }

        public int getWaitingClock()
        {
            return waitingClock;
        }

        public void setWaitingClock(int waitingClock)
        {
            this.waitingClock = waitingClock;
        }


        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getPrioritas()
        {
            return prioritas;
        }

        public void setPrioritas(int prioritas)
        {
            this.prioritas = prioritas;
        }

        public int getLamaProses()
        {
            return lamaProses;
        }

        public void setLamaProses(int lamaProses)
        {
            this.lamaProses = lamaProses;
        }

        public int getBurstTime()
        {
            return burstTime;
        }

        public void setBurstTime(int burstTime)
        {
            this.burstTime = burstTime;
        }

        public int getAksesIO()
        {
            return aksesIO;
        }

        public void setAksesIO(int aksesIO)
        {
            this.aksesIO = aksesIO;
        }

        public int getClockAwal()
        {
            return clockAwal;
        }

        public void setClockAwal(int clockAwal)
        {
            this.clockAwal = clockAwal;
        }

    }



}
