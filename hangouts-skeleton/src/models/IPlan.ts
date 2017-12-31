export interface IPlan {
    id: number,
    groupId: number,
    activityId: number,
    chatId: number, 
    startTime: Date,
    endTime: Date,
    address: {
        id: number,
        latitude: number,
        longitude: number,
        location: string
    }, 
    status: string
} 