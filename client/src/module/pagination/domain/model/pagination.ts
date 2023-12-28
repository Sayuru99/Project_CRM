interface TablePaginationI {
  page: number;
  itemsPerPage: number;
  sort: string;
  descending: string;
  search: string;
}

class TablePagination {
  page: number;
  itemsPerPage: number;
  sort: string;
  descending: string;
  search: string;

  constructor({
    page,
    itemsPerPage,
    sort,
    descending,
    search,
  }: TablePaginationI) {
    this.page = page;
    this.itemsPerPage = itemsPerPage;
    this.descending = descending;
    this.search = search;
    this.sort = sort;
  }
}

export { TablePagination };
