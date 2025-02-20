export class UserTaskVM {
    id!: number
    userId?: string
    user?: string
    taskId: number = 0
    title?: string
    date?: Date
    parent?: string
    claimId: number = 0
    claim?: string
    sp: number
    comments?: string
    reviewedBy?: string
    reviewComments?: string
    isActive?: boolean
    selectedTask: boolean = false;
    isDayEnded: boolean
    isChecked?: boolean; // Add this property

}