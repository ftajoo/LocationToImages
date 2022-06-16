export enum PhotoSearchBy {
  Title,
  Description,
  Address,
  All
}

export const PhotoSearchByMapping: Record<(string | PhotoSearchBy), string> = {
  [PhotoSearchBy.All]: 'All',
  [PhotoSearchBy.Title]: 'Title',
  [PhotoSearchBy.Description]: 'Description',
  [PhotoSearchBy.Address]: 'Address',
};
