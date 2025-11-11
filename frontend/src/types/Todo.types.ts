export interface TodoItem {
    id: number,
    name: string,
    isComplete: boolean
};

export type CreateItemToDo = Omit<TodoItem, "id">;