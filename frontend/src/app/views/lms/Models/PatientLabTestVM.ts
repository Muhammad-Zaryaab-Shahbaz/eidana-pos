export class PatientLabTestVM {
    clientId!: number
    labTestId: number
    sampleTypeId: number
    patientId: number
    doctorId: number
    testDate: Date
    remarks?: string
    price: Float32Array 
    discountAmt: Float32Array 
    paidAmt: Float32Array 
    resultDate: Date
    resultValue:string
    isActive?: boolean
	
}