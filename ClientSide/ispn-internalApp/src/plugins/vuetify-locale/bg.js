export default {
  badge: 'Бадж',
  close: 'Затвори',
  dataIterator: {
    noResultsText: 'Няма намерени резултати',
    loadingText: 'Моля изчакайте...',
  },
  dataTable: {
    itemsPerPageText: 'Резултати на страница:',
    ariaLabel: {
      sortDescending: 'Низходящо сортиране.',
      sortAscending: 'Възходящо сортиране.',
      sortNone: 'Без сортиране.',
      activateNone: 'Премахни сортиране.',
      activateDescending: 'Активирай за низходящо сортиране.',
      activateAscending: 'Активирай за възходящо сортиране.',
    },
    sortBy: 'Сортирай по',
  },
  dataFooter: {
    itemsPerPageText: 'Резултати на страница:',
    itemsPerPageAll: 'Всички',
    nextPage: 'Следваща страница',
    prevPage: 'Предишна страница',
    firstPage: 'Начална страница',
    lastPage: 'Последна страница',
    pageText: '{0}-{1} от {2}',
  },
  datePicker: {
    itemsSelected: '{0} селектирани',
    nextMonthAriaLabel: 'Следващ месец',
    nextYearAriaLabel: 'Следваща година',
    prevMonthAriaLabel: 'Преден месец',
    prevYearAriaLabel: 'Предна година',
  },
  noDataText: 'Няма налични данни',
  carousel: {
    prev: 'Следващ',
    next: 'Предишен',
    ariaLabel: {
      delimiter: 'Слайд {0} от {1}',
    },
  },
  calendar: {
    moreEvents: '{0} още',
  },
  fileInput: {
    counter: '{0} файлове',
    counterSize: '{0} файлове ({1} общо)',
  },
  timePicker: {
    am: 'AM',
    pm: 'PM',
  },
  pagination: {
    ariaLabel: {
      wrapper: 'Навигация',
      next: 'Следваща страница',
      previous: 'Предишна страница',
      page: 'Отиди на страница {0}',
      currentPage: 'Сегашна страница, Страница {0}',
    },
  },
  rating: {
    ariaLabel: {
      icon: 'Рейтинг {0} от {1}',
    },
  },
  extensions: {
    Blockquote: {
      buttons: {
        blockquote: {
          tooltip: 'Block quote'
        }
      }
    },
    Bold: {
      buttons: {
        bold: {
          tooltip: 'Bold'
        }
      }
    },
    BulletList: {
      buttons: {
        bulletList: {
          tooltip: 'Bulleted list'
        }
      }
    },
    Code: {
      buttons: {
        code: {
          tooltip: 'Code'
        }
      }
    },
    CodeBlock: {
      buttons: {
        codeBlock: {
          tooltip: 'Code block'
        }
      }
    },
    History: {
      buttons: {
        undo: {
          tooltip: 'Undo'
        },
        redo: {
          tooltip: 'Redo'
        }
      }
    },
    HorizontalRule: {
      buttons: {
        horizontalRule: {
          tooltip: 'Horizontal line'
        }
      }
    },
    Italic: {
      buttons: {
        italic: {
          tooltip: 'Italic'
        }
      }
    },
    OrderedList: {
      buttons: {
        orderedList: {
          tooltip: 'Ordered list'
        }
      }
    },
    Paragraph: {
      buttons: {
        paragraph: {
          tooltip: 'Paragraph'
        }
      }
    },
    Strike: {
      buttons: {
        strike: {
          tooltip: 'Strike'
        }
      }
    },
    Underline: {
      buttons: {
        underline: {
          tooltip: 'Underline'
        }
      }
    },
    Heading: {
      buttons: {
        heading: {
          tooltip: ({ level }) => level + ' level header'
        }
      }
    },
    Link: {
      buttons: {
        isActive: {
          tooltip: 'Change Link'
        },
        notActive: {
          tooltip: 'Add Link'
        }
      },
      window: {
        title: 'Link control',
        form: {
          hrefLabel: 'URL'
        },
        buttons: {
          close: 'Close',
          remove: 'Remove',
          apply: 'Apply'
        }
      }
    },
    Image: {
      buttons: {
        tooltip: 'Image'
      },
      window: {
        title: 'Add Image',
        form: {
          sourceLink: 'Image URL',
          altText: 'Alternative Text',
          addImage: 'Add Image'
        },
        imageUpload: {
          instruction: 'Choose a file(s) or drag it here.'
        },
        buttons: {
          close: 'Close',
          apply: 'Apply'
        }
      }
    },
    TodoList: {
      buttons: {
        todoList: {
          tooltip: 'To Do List'
        }
      }
    }
  }
}
