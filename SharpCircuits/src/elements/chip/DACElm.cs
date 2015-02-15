using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpCircuit {

	public class DACElm : ChipElm {

		public DACElm() : base() {

		}

		public override String getChipName() {
			return "DAC";
		}

		public override bool needsBits() {
			return true;
		}

		public override void setupPins() {
			pins = new Pin[getLeadCount()];
			for(int i = 0; i != bits; i++)
				pins[i] = new Pin("D" + i);
			pins[bits] = new Pin("O");
			pins[bits].output = true;
			pins[bits + 1] = new Pin("V+");
			allocNodes();
		}

		public override void doStep(CirSim sim) {
			int ival = 0;
			for(int i = 0; i != bits; i++)
				if(volts[i] > 2.5)
					ival |= 1 << i;
			int ivalmax = (1 << bits) - 1;
			double v = ival * volts[bits + 1] / ivalmax;
			sim.updateVoltageSource(0, nodes[bits], pins[bits].voltSource, v);
		}

		public override int getVoltageSourceCount() {
			return 1;
		}

		public override int getLeadCount() {
			return bits + 2;
		}

	}
}